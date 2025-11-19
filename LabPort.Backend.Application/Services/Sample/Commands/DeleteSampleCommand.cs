using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sample.Commands
{
    public class DeleteSampleCommand : IRequest
    {
        public Guid SampleId { get; set; }

        public DeleteSampleCommand(Guid sampleId)
        {
            SampleId = sampleId;
        }
    }

    public class DeleteSampleCommandHandler : IRequestHandler<DeleteSampleCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public DeleteSampleCommandHandler(ILabPortDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if(userId == null)
            {
                throw new Exception("User is not authenticated");
            }

            var sample = await _context.Samples
                .FirstOrDefaultAsync(s => s.Id == request.SampleId 
                && s.Container.UserId == userId.Value, cancellationToken);

            if(sample == null)
            {
                throw new Exception($"Sample with Id {request.SampleId} was not found");
            }

            _context.Samples.Remove(sample);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
