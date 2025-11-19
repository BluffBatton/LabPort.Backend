using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Command
{
    public class DeleteSourceTypeCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteSourceTypeCommand(Guid id)
        {
            this.Id = id;
        }
    }

    public class DeleteSourceTypeCommandHandler : IRequestHandler<DeleteSourceTypeCommand>
    {
        private readonly ILabPortDbContext _context;
        public DeleteSourceTypeCommandHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteSourceTypeCommand request, CancellationToken cancellationToken)
        {
            var sourceType = await _context.SourceTypes.FirstOrDefaultAsync(st => st.Id == request.Id, cancellationToken);
            if(sourceType == null)
            {
                throw new KeyNotFoundException($"SourceType with id {request.Id} was not found");
            }

            bool isUsed = await _context.Sources.AnyAsync(s => s.SourceTypeId == request.Id, cancellationToken);

            if (isUsed)
            {
                throw new InvalidOperationException($"Source Type {sourceType.Name} cannot be deleted because it's used by existing Sources");
            }

            _context.SourceTypes.Remove(sourceType);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
