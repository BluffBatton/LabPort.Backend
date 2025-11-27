using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LabPort.Backend.Application.Services.Admin.Queries
{
    public record ExportUsersCsvQuery(DateTime? From, DateTime? To) : IRequest<byte[]>;

    public class ExportUsersCsvQueryHandler : IRequestHandler<ExportUsersCsvQuery, byte[]>
    {
        private readonly ILabPortDbContext _context;

        public ExportUsersCsvQueryHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> Handle(ExportUsersCsvQuery request, CancellationToken ct)
        {
            var from = request.From ?? DateTime.MinValue;
            var to = request.To ?? DateTime.UtcNow;

            var data = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    FullName = u.FirstName + " " + u.LastName,
                    Samples = _context.Samples.Count(s =>
                        s.Container.UserId == u.Id &&
                        s.CreatedAt >= from && s.CreatedAt <= to),
                    Tests = _context.Tests.Count(t =>
                        t.Sample.Container.UserId == u.Id &&
                        t.CreatedAt >= from && t.CreatedAt <= to),
                    Alerts = _context.Alerts.Count(a =>
                        a.SensorReading.Sensor.Container.UserId == u.Id &&
                        a.CreatedAt >= from && a.CreatedAt <= to)
                })
                .AsNoTracking()
                .ToListAsync(ct);

            // CSV building
            var sb = new StringBuilder();
            sb.AppendLine("UserId,Email,FullName,SamplesCount,TestsCount,AlertsCount");

            foreach (var r in data)
            {
                sb.AppendLine($"{r.Id},{r.Email},{r.FullName},{r.Samples},{r.Tests},{r.Alerts}");
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
