using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.TestResult.Commands
{
    public class CreateTestResultCommand : IRequest
    {
        public TestResultCreateDto Dto { get; set; }

        public CreateTestResultCommand(TestResultCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateTestResultCommandHandler : IRequestHandler<CreateTestResultCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITestResultEvaluator _evaluator;

        public CreateTestResultCommandHandler(
            ILabPortDbContext context, 
            IMapper mapper,
            ITestResultEvaluator evaluator)
        {
            _context = context;
            _mapper = mapper;
            _evaluator = evaluator;
        }

        public async Task Handle(CreateTestResultCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var test = await _context.Tests
                .Include(t => t.TestType)
                .FirstOrDefaultAsync(t => t.Id == dto.TestId, cancellationToken);

            if (test == null)
            {
                throw new Exception($"Test with ID {dto.TestId} does not exist");

            }

            var exists = await _context.TestResults
                .AnyAsync(r => r.TestId == dto.TestId, cancellationToken);

            if (exists)
            {
                throw new Exception($"Test with ID {dto.TestId} already has a result");

            }

            var result = _mapper.Map<Domain.Entities.TestResult>(dto);
            result.TestId = dto.TestId;


            result.ResultStatus = _evaluator.Evaluate(test, dto.ValueNumeric);

            await _context.TestResults.AddAsync(result, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}