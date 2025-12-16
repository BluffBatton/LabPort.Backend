using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sensor.Commands
{
    public class ToggleLidCommand : IRequest<LidPosition>
    {
    }

    public class ToggleLidCommandHandler : IRequestHandler<ToggleLidCommand, LidPosition>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMqttService _mqttService;
        private readonly IUserContextService _userContextService;

        public ToggleLidCommandHandler(
            ILabPortDbContext context,
            IMqttService mqttService,
            IUserContextService userContextService)
        {
            _context = context;
            _mqttService = mqttService;
            _userContextService = userContextService;
        }

        public async Task<LidPosition> Handle(ToggleLidCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var sensor = await _context.Sensors
                .Include(s => s.Container)
                .FirstOrDefaultAsync(s => s.Container.UserId == userId.Value, cancellationToken);

            if (sensor == null)
            {
                throw new Exception("Sensor was not found");
            }
            var currentDomainState = sensor.CurrentLidPosition;

            var newDomainState = currentDomainState == Domain.Enums.LidPosition.Open
                ? Domain.Enums.LidPosition.Closed
                : Domain.Enums.LidPosition.Open;

            sensor.CurrentLidPosition = newDomainState;
            await _context.SaveChangesAsync(cancellationToken);

            string anglePayload = newDomainState == Domain.Enums.LidPosition.Open ? "90" : "0";
            string topic = $"labport/{sensor.DeviceKey}/servo";

            await _mqttService.PublishAsync(topic, anglePayload);

            return (LidPosition)newDomainState;
        }
    }
}