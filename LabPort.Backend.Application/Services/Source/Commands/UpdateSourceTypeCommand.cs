using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Command
{
    public class UpdateSourceTypeCommand : IRequest<SourceTypeDto>
    {
        public Guid SourceTypeId { get; }
        public SourceTypeUpdateDto Dto { get; }

        public UpdateSourceTypeCommand(Guid sourceTypeId, SourceTypeUpdateDto dto)
        {
            SourceTypeId = sourceTypeId;
            Dto = dto;
        }
    }

    public class UpdateSourceTypeCommandHandler : IRequestHandler<UpdateSourceTypeCommand, SourceTypeDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSourceTypeCommandHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SourceTypeDto> Handle(UpdateSourceTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto == null)
            {
                throw new InvalidOperationException("Source type update body is required.");
            }

            var name = request.Dto.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Source type name is required.");
            }

            var duplicateExists = await _context.SourceTypes
                .AnyAsync(st => st.Id != request.SourceTypeId && st.Name.ToLower() == name.ToLower(), cancellationToken);

            if (duplicateExists)
            {
                throw new InvalidOperationException($"Source type with name '{name}' already exists.");
            }

            var sourceType = await _context.SourceTypes
                .FirstOrDefaultAsync(st => st.Id == request.SourceTypeId, cancellationToken);

            if (sourceType == null)
            {
                throw new KeyNotFoundException($"Source type with id {request.SourceTypeId} was not found.");
            }

            sourceType.Name = name;
            sourceType.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SourceTypeDto>(sourceType);
        }
    }
}
