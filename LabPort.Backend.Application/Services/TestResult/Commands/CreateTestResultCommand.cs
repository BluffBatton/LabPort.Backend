using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
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

        public CreateTestResultCommandHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(CreateTestResultCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var testExists = await _context.Tests
                .AnyAsync(t => t.Id == dto.TestId, cancellationToken);

            if (!testExists)
            {
                throw new Exception($"Test with ID {dto.TestId} does not exist");
            }
                
            var result = _mapper.Map<Domain.Entities.TestResult>(dto);

            await _context.TestResults.AddAsync(result, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
