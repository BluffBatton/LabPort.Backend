using LabPort.Backend.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LabPort.Backend.Infrastructure.Integration.Background
{
    public class SensorReadingCleanupWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SensorReadingCleanupWorker> _logger;
        private readonly TimeSpan _interval;
        private readonly TimeSpan _maxAge;

        public SensorReadingCleanupWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<SensorReadingCleanupWorker> logger,
            IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            var intervalMinutesStr = configuration.GetSection("SensorCleanup:IntervalMinutes").Value;
            var maxAgeDaysStr = configuration.GetSection("SensorCleanup:MaxAgeDays").Value;

            var intervalMinutes = !string.IsNullOrEmpty(intervalMinutesStr) ? int.Parse(intervalMinutesStr) : 60;
            var maxAgeDays = !string.IsNullOrEmpty(maxAgeDaysStr) ? int.Parse(maxAgeDaysStr) : 7;

            _interval = TimeSpan.FromMinutes(intervalMinutes);
            _maxAge = TimeSpan.FromDays(maxAgeDays);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SensorReadingCleanupWorker started. Interval: {Interval}, MaxAge: {MaxAge}", _interval, _maxAge);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var cleanupService = scope.ServiceProvider.GetRequiredService<ISensorReadingCleanupService>();

                    await cleanupService.CleanupOldReadingsAsync(_maxAge, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during SensorReading cleanup.");
                }

                try
                {
                    await Task.Delay(_interval, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                }
            }

            _logger.LogInformation("SensorReadingCleanupWorker is stopping.");
        }
    }
}
