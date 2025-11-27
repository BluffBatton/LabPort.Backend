using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence;

namespace LabPort.Backend.Infrastructure.Integration.Alerts
{
    public class AlertService : IAlertService
    {
        private readonly LabPortDbContext _context;

        public AlertService(LabPortDbContext context)
        {
            _context = context;
        }

        public async Task CheckAndCreateAlertsAsync(
            Container container,
            SensorReading reading,
            CancellationToken cancellationToken)
        {
            var alerts = new List<Alert>();

            if (reading.Temperature < container.TemperatureMin ||
                reading.Temperature > container.TemperatureMax)
            {
                alerts.Add(new Alert
                {
                    Message = $"Температура {reading.Temperature}°C виходить за межі " +
                              $"[{container.TemperatureMin}; {container.TemperatureMax}]",
                    SensorReadingId = reading.Id,
                    SensorReading = reading,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                });
            }

            if (reading.Humidity < container.HumidityMin ||
                reading.Humidity > container.HumidityMax)
            {
                alerts.Add(new Alert
                {
                    Message = $"Вологість {reading.Humidity}% виходить за межі " +
                              $"[{container.HumidityMin}; {container.HumidityMax}]",
                    SensorReadingId = reading.Id,
                    SensorReading = reading,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                });
            }

            if (alerts.Count > 0)
            {
                await _context.Alerts.AddRangeAsync(alerts, cancellationToken);
            }
        }
    }
}
