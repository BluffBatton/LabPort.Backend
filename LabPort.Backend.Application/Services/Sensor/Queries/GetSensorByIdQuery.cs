using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sensor.Queries
{
    public class GetSensorByIdQuery : IRequest<SensorDto>
    {
        public Guid SensorId { get; set; }
        public GetSensorByIdQuery(Guid sensorId) 
        {
            SensorId = sensorId;
        }
    }

    public class GetSensorByIdQueryHandler : IRequestHandler<GetSensorByIdQuery, SensorDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetSensorByIdQueryHandler(
            ILabPortDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SensorDto> Handle(GetSensorByIdQuery request, CancellationToken cancellationToken)
        {
            var sensor = await _context.Sensors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.SensorId, cancellationToken);

            if (sensor == null)
                throw new Exception($"Sensor with ID {request.SensorId} not found.");

            return _mapper.Map<SensorDto>(sensor);
        }
    }
}
