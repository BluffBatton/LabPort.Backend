using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Test.Commands
{
    public class CreateTestCommand : IRequest
    {
        public TestCreateDto Dto { get; set; }
        public CreateTestCommand(TestCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(
            ILabPortDbContext context, 
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var sampleExists = await _context.Samples.AnyAsync(
                s => s.Id == request.Dto.SampleId,
                cancellationToken
            );

            if (!sampleExists)
            {
                throw new Exception($"Sample {request.Dto.SampleId} not found");
            }
            var testTypeExists = await _context.TestTypes.AnyAsync(
                t => t.Id == request.Dto.TestTypeId,
                cancellationToken
            );

            if (!testTypeExists)
            {
                throw new Exception($"TestType {request.Dto.TestTypeId} not found");
            }
            
            var test = _mapper.Map<Domain.Entities.Test>(request.Dto);

            _context.Tests.Add(test);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}