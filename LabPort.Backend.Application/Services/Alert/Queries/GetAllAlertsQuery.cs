using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Alert.Queries
{
    public class GetAllAlertsQuery : IRequest<List<AlertDto>>
    {
    }

    public class GetAllAlertsQueryHandler : IRequestHandler<GetAllAlertsQuery, List<AlertDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public GetAllAlertsQueryHandler(
            ILabPortDbContext context,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<List<AlertDto>> Handle(GetAllAlertsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var query = _context.Alerts
                .Include(a => a.SensorReading)
                    .ThenInclude(sr => sr.Sensor)
                        .ThenInclude(s => s.Container)
                .Where(a => a.SensorReading.Sensor.Container.UserId == userId.Value);

            return await query
                .OrderBy(a => a.IsRead)
                .ThenByDescending(a => a.CreatedAt)
                .AsNoTracking()
                .ProjectTo<AlertDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
