using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sensor.Queries
{
    public record GetSensorStatusQuery(string DeviceKey) : IRequest<SensorStatusDto?>;

    public class GetSensorStatusQueryHandler : IRequestHandler<GetSensorStatusQuery, SensorStatusDto?>
    {
        private readonly ILabPortDbContext _context;

        public GetSensorStatusQueryHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task<SensorStatusDto?> Handle(GetSensorStatusQuery request, CancellationToken cancellationToken)
        {
            var sensor = await _context.Sensors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.DeviceKey.ToLower() == request.DeviceKey.ToLower().Trim(), cancellationToken);

            if (sensor == null)
                return null;

            return new SensorStatusDto
            {
                DeviceKey = sensor.DeviceKey,
                CurrentLidPosition = sensor.CurrentLidPosition.ToString()
            };
        }
    }
}
