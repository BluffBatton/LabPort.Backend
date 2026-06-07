using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabPort.Backend.Infrastructure.Integration.Alerts
{
    public class AlertService : IAlertService
    {
        private readonly LabPortDbContext _context;
        private readonly ILogger<AlertService> _logger;

        public AlertService(LabPortDbContext context, ILogger<AlertService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CheckAndCreateAlertsAsync(
            Container container,
            SensorReading reading,
            CancellationToken cancellationToken)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (reading == null) throw new ArgumentNullException(nameof(reading));

            var alerts = new List<Alert>();

            if (reading.Temperature < container.TemperatureMin ||
                reading.Temperature > container.TemperatureMax)
            {
                var message = $"Температура {reading.Temperature}°C виходить за межі [{container.TemperatureMin}; {container.TemperatureMax}]";
                alerts.Add(new Alert
                {
                    Message = message,
                    SensorReadingId = reading.Id,
                    SensorReading = reading,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                });
            }

            if (reading.Humidity < container.HumidityMin ||
                reading.Humidity > container.HumidityMax)
            {
                var message = $"Вологість {reading.Humidity}% виходить за межі [{container.HumidityMin}; {container.HumidityMax}]";
                alerts.Add(new Alert
                {
                    Message = message,
                    SensorReadingId = reading.Id,
                    SensorReading = reading,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                });
            }

            if (alerts.Count == 0)
            {
                _logger.LogDebug("No alerts created for reading {ReadingId}", reading.Id);
                return;
            }

            try
            {
                var newAlerts = new List<Alert>();
                foreach (var a in alerts)
                {
                    bool exists = await _context.Alerts
                        .AsNoTracking()
                        .AnyAsync(x => x.SensorReadingId == a.SensorReadingId && x.Message == a.Message, cancellationToken);

                    if (exists)
                    {
                        _logger.LogDebug("Skipped duplicate alert for reading {ReadingId}: {Message}", a.SensorReadingId, a.Message);
                        continue;
                    }

                    newAlerts.Add(a);
                }

                if (newAlerts.Count == 0)
                {
                    _logger.LogDebug("All alerts were duplicates and skipped for reading {ReadingId}", reading.Id);
                    return;
                }

                await _context.Alerts.AddRangeAsync(newAlerts, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Created {Count} alerts for reading {ReadingId}", newAlerts.Count, reading.Id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Alert creation cancelled for reading {ReadingId}", reading.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating alerts for reading {ReadingId}", reading.Id);
                throw;
            }
        }
    }
}
