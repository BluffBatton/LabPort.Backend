using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Test.Commands
{
    public class CreateTestTypeCommand : IRequest
    {
        public TestTypeCreateDto TestType { get; set; }

        public CreateTestTypeCommand(TestTypeCreateDto testType)
        {
            TestType = testType;
        }
    }

    public class CreateTestTypeCommandHandler : IRequestHandler<CreateTestTypeCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public CreateTestTypeCommandHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(CreateTestTypeCommand request, CancellationToken cancellationToken)
        {
            var typeExists = await _context.TestTypes.AnyAsync(tt => tt.Name == request.TestType.Name);
            if (typeExists)
            {
                throw new Exception($"Type with name '{request.TestType.Name}' already exists");
            }

            var testType = _mapper.Map<TestType>(request.TestType);
            testType.CreatedAt = DateTime.UtcNow;

            await _context.TestTypes.AddAsync(testType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}