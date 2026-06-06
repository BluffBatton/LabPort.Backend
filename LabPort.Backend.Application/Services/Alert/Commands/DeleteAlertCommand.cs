using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Alert.Commands
{
    public class DeleteAlertCommand : IRequest
    {
        public Guid AlertId { get; }

        public DeleteAlertCommand(Guid alertId)
        {
            AlertId = alertId;
        }
    }

    public class DeleteAlertCommandHandler : IRequestHandler<DeleteAlertCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public DeleteAlertCommandHandler(ILabPortDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(DeleteAlertCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var alert = await _context.Alerts
                .Include(a => a.SensorReading)
                    .ThenInclude(sr => sr.Sensor)
                        .ThenInclude(s => s.Container)
                .FirstOrDefaultAsync(
                    a => a.Id == request.AlertId &&
                         a.SensorReading.Sensor.Container.UserId == userId.Value,
                    cancellationToken);

            if (alert == null)
            {
                throw new KeyNotFoundException($"Alert with id {request.AlertId} was not found or does not belong to current user.");
            }

            alert.DeletedAt = DateTime.UtcNow;
            alert.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
