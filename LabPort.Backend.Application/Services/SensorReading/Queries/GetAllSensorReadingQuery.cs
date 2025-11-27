using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.SensorReading.Queries
{
    public class GetAllSensorReadingsQuery : IRequest<List<SensorReadingDto>>
    {
        public int? Take { get; }

        public GetAllSensorReadingsQuery(int? take = null)
        {
            Take = take;
        }
    }

    public class GetAllSensorReadingsQueryHandler : IRequestHandler<GetAllSensorReadingsQuery, List<SensorReadingDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public GetAllSensorReadingsQueryHandler(
            ILabPortDbContext context,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<List<SensorReadingDto>> Handle(GetAllSensorReadingsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var sensorId = await _context.Sensors
                .Where(s => s.Container.UserId == userId.Value)
                .Select(s => (Guid?)s.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (sensorId == null)
            {
                return new List<SensorReadingDto>();
            }

            var take = request.Take ?? 100;

            return await _context.SensorReadings
                .Where(r => r.SensorId == sensorId.Value)
                .OrderByDescending(r => r.CreatedAt)
                .Take(take)
                .AsNoTracking()
                .ProjectTo<SensorReadingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
