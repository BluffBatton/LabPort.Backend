using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sensor.Queries
{
    public class GetMySensorQuery : IRequest<SensorDto?>
    {
    }

    public class GetMySensorQueryHandler : IRequestHandler<GetMySensorQuery, SensorDto?>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public GetMySensorQueryHandler(
            ILabPortDbContext context,
            IUserContextService userContext,
            IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<SensorDto?> Handle(GetMySensorQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUserId();

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");

            }

            var sensor = await _context.Sensors
                .Include(s => s.Container)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Container.UserId == userId.Value, cancellationToken);

            if (sensor == null)
            {
                throw new Exception("Sensor is null");
            }

            return _mapper.Map<SensorDto>(sensor);
        }
    }
}
