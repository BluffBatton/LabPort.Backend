using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Statistics;
using LabPort.Backend.Contracts.DTOs.Statistics.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Admin.Queries
{
    public class GetAdminStatisticsQuery : IRequest<AdminStatisticsDto>
    {
        public int Days { get; set; } = 7;
    }

    public class GetAdminStatisticsQueryHandler
        : IRequestHandler<GetAdminStatisticsQuery, AdminStatisticsDto>
    {
        private readonly ILabPortDbContext _context;

        public GetAdminStatisticsQueryHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task<AdminStatisticsDto> Handle(
            GetAdminStatisticsQuery request,
            CancellationToken cancellationToken)
        {
            var days = request.Days <= 0 ? 7 : request.Days;
            var since = DateTime.UtcNow.AddDays(-days);

            var totalUsers = await _context.Users.CountAsync(cancellationToken);

            var activeUsers = await _context.Users
                .CountAsync(u => u.LastLoginAt != null && u.LastLoginAt >= since, cancellationToken);

            var totalSamples = await _context.Samples.CountAsync(cancellationToken);
            var totalTests = await _context.Tests.CountAsync(cancellationToken);

            var totalAlerts = await _context.Alerts.CountAsync(cancellationToken);

            return new AdminStatisticsDto
            {
                TotalUsers = totalUsers,
                ActiveUsersLastNDays = activeUsers,
                TotalSamples = totalSamples,
                TotalTests = totalTests,
                TotalAlerts = totalAlerts,
                DaysWindow = days
            };
        }
    }
}
