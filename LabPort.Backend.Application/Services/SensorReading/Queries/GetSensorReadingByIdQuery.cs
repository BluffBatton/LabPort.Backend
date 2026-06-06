using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.SensorReading.Queries
{
    public class GetSensorReadingByIdQuery : IRequest<SensorReadingDto>
    {
        public Guid Id { get; }

        public GetSensorReadingByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetSensorReadingByIdQueryHandler : IRequestHandler<GetSensorReadingByIdQuery, SensorReadingDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public GetSensorReadingByIdQueryHandler(
            ILabPortDbContext context,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<SensorReadingDto> Handle(GetSensorReadingByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var reading = await _context.SensorReadings
                .Include(r => r.Sensor)
                    .ThenInclude(s => s.Container)
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    r => r.Id == request.Id &&
                         r.Sensor.Container.UserId == userId.Value,
                    cancellationToken);

            if (reading == null)
            {
                throw new KeyNotFoundException($"SensorReading with id {request.Id} was not found or does not belong to current user.");
            }

            return _mapper.Map<SensorReadingDto>(reading);
        }
    }
}
