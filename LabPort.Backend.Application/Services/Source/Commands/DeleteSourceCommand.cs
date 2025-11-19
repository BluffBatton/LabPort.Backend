using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Command
{
    public class DeleteSourceCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteSourceCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand> 
    {
        private readonly ILabPortDbContext _context;

        public DeleteSourceCommandHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
        {
            var source = await _context.Sources.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (source == null)
            {
                throw new Exception($"Source with Id {request.Id} was not found");
            }

            _context.Sources.Remove(source);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
