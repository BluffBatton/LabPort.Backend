using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Statistics.Admin;
using LabPort.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Admin.Queries
{
    public class GetUserItemStatisticsQuery : IRequest<AdminUserReportItemDto>
    {
        public Guid UserId { get; set; }

        public GetUserItemStatisticsQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserItemStatisticsQueryHandler : IRequestHandler<GetUserItemStatisticsQuery, AdminUserReportItemDto>
    {
        private readonly ILabPortDbContext _context;

        public GetUserItemStatisticsQueryHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task<AdminUserReportItemDto> Handle(GetUserItemStatisticsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User with Id {request.UserId} was not found");
            }

            var samplesCount = await _context.Samples
                .AsNoTracking()
                .CountAsync(s => s.Container != null &&
                                 s.Container.UserId == request.UserId,
                            cancellationToken);

            var testsCount = await _context.Tests
                .AsNoTracking()
                .CountAsync(t => t.Sample != null &&
                                 t.Sample.Container != null &&
                                 t.Sample.Container.UserId == request.UserId,
                            cancellationToken);

            var alertsCount = await _context.Alerts
                .AsNoTracking()
                .CountAsync(a => a.SensorReading != null &&
                                 a.SensorReading.Sensor != null &&
                                 a.SensorReading.Sensor.Container != null &&
                                 a.SensorReading.Sensor.Container.UserId == request.UserId,
                            cancellationToken);

            return new AdminUserReportItemDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                SamplesCount = samplesCount,
                TestsCount = testsCount,
                AlertsCount = alertsCount
            };
        }
    }
}
