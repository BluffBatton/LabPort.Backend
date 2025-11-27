using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sensor.Commands
{
    public class UpdateSensorCommand : IRequest
    {
        public SensorUpdateDto Sensor { get; set; }
        public UpdateSensorCommand(SensorUpdateDto sensor)
        {
            Sensor = sensor;
        }
    }

    public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public UpdateSensorCommandHandler(ILabPortDbContext context,  IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if (userId == null)
                throw new UnauthorizedAccessException("User is not authenticated");

            var sensor = await _context.Sensors
                .Include(s => s.Container)
                .FirstOrDefaultAsync(s => s.Container.UserId == userId.Value, cancellationToken);

            if (sensor == null)
                throw new Exception("You do not have a sensor to update.");

            var dto = request.Sensor;

            if (!string.IsNullOrEmpty(dto.SerialName))
            {
                sensor.SerialName = dto.SerialName;
            }

            if (!string.IsNullOrEmpty(dto.DeviceKey))
            {
                sensor.DeviceKey = dto.DeviceKey;

            }

            sensor.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
