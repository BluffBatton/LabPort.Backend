using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Statistics;
using LabPort.Backend.Contracts.DTOs.Statistics.Container;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Container.Queries
{
    public class GetContainerReadingsStatsQuery : IRequest<ContainerReadingStatsDto>
    {
        // last7days, day, hour
        public string Range { get; set; } = "last7days";
    }

    public class GetContainerReadingsStatsQueryHandler
        : IRequestHandler<GetContainerReadingsStatsQuery, ContainerReadingStatsDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public GetContainerReadingsStatsQueryHandler(
            ILabPortDbContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<ContainerReadingStatsDto> Handle(
            GetContainerReadingsStatsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var container = await _context.Containers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId.Value, cancellationToken);

            if (container == null)
            {
                throw new Exception("Current user has no container configured.");
            }

            var now = DateTime.UtcNow;
            var from = request.Range.ToLower() switch
            {
                "hour" => now.AddHours(-1),
                "day" => now.AddDays(-1),
                "last7days" => now.AddDays(-7),
                _ => now.AddDays(-7) 
            };

            var readings = await _context.SensorReadings
                .Include(r => r.Sensor)
                .Where(r =>
                    r.Sensor.ContainerId == container.Id &&
                    r.CreatedAt >= from &&
                    r.CreatedAt <= now)
                .OrderBy(r => r.CreatedAt)
                .ToListAsync(cancellationToken);

            var result = new ContainerReadingStatsDto
            {
                ContainerId = container.Id,
                From = from,
                To = now,
                Points = readings.Select(r => new ReadingPointDto
                {
                    Time = r.CreatedAt,
                    Temperature = r.Temperature,
                    Humidity = r.Humidity
                }).ToList()
            };

            if (readings.Count > 0)
            {
                var temps = readings.Select(r => (double?)r.Temperature).ToList();
                var hums = readings.Select(r => (double?)r.Humidity).ToList();

                result.TempMin = temps.Min();
                result.TempMax = temps.Max();
                result.TempAvg = temps.Average();

                result.HumMin = hums.Min();
                result.HumMax = hums.Max();
                result.HumAvg = hums.Average();
            }

            return result;
        }
    }
}
