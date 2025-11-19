using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Command
{
    public class UpdateSourceCommand : IRequest
    {
        public Guid SourceId { get; }
        public SourceUpdateDto Source { get; }

        public UpdateSourceCommand(Guid sourceId, SourceUpdateDto source)
        {
            SourceId = sourceId;
            Source = source;
        }
    }

    public class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand> 
    {
        private readonly ILabPortDbContext _context;

        public UpdateSourceCommandHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
        {
            var source = await _context.Sources.FirstOrDefaultAsync(s => s.Id == request.SourceId, cancellationToken);

            if (source == null)
            {
                throw new Exception($"Source with Id {request.SourceId} was not found");
            }

            var dto = request.Source;

            if (!string.IsNullOrEmpty(dto.Name))
            {
                source.Name = dto.Name;
            }
            if (!string.IsNullOrEmpty(dto.Note))
            {
                source.Note = dto.Note;
            }
            if (!string.IsNullOrEmpty(dto.Location))
            {
                source.Location = dto.Location;
            }
            if (!string.IsNullOrEmpty(dto.ContactInfo))
            {
                source.ContactInfo = dto.ContactInfo;
            }
            if (dto.SourceTypeId.HasValue)
            {
                source.SourceTypeId = dto.SourceTypeId.Value;
            }

            source.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}