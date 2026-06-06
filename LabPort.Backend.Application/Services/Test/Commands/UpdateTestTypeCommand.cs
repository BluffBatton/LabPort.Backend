using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Test.Commands
{
    public class UpdateTestTypeCommand : IRequest<TestTypeDto>
    {
        public Guid TestTypeId { get; }
        public TestTypeUpdateDto Dto { get; }

        public UpdateTestTypeCommand(Guid testTypeId, TestTypeUpdateDto dto)
        {
            TestTypeId = testTypeId;
            Dto = dto;
        }
    }

    public class UpdateTestTypeCommandHandler : IRequestHandler<UpdateTestTypeCommand, TestTypeDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTestTypeCommandHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TestTypeDto> Handle(UpdateTestTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto == null)
            {
                throw new InvalidOperationException("Test type update body is required.");
            }

            var name = request.Dto.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Test type name is required.");
            }

            var duplicateExists = await _context.TestTypes
                .AnyAsync(tt => tt.Id != request.TestTypeId && tt.Name.ToLower() == name.ToLower(), cancellationToken);

            if (duplicateExists)
            {
                throw new InvalidOperationException($"Test type with name '{name}' already exists.");
            }

            var testType = await _context.TestTypes
                .FirstOrDefaultAsync(tt => tt.Id == request.TestTypeId, cancellationToken);

            if (testType == null)
            {
                throw new KeyNotFoundException($"Test type with id {request.TestTypeId} was not found.");
            }

            testType.Name = name;
            testType.ReferenceMin = request.Dto.ReferenceMin;
            testType.ReferenceMax = request.Dto.ReferenceMax;
            testType.Unit = request.Dto.Unit;
            testType.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TestTypeDto>(testType);
        }
    }
}
