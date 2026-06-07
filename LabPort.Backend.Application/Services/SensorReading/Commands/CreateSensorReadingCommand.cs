using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.SensorReading.Commands
{
    public class CreateSensorReadingCommand : IRequest
    {
        public SensorReadingCreateDto SensorReading { get; }

        public CreateSensorReadingCommand(SensorReadingCreateDto sensorReading)
        {
            SensorReading = sensorReading;
        }
    }

    public class CreateSensorReadingCommandHandler : IRequestHandler<CreateSensorReadingCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlertService _alertService;

        public CreateSensorReadingCommandHandler(
            ILabPortDbContext context,
            IMapper mapper,
            IAlertService alertService)
        {
            _context = context;
            _mapper = mapper;
            _alertService = alertService;
        }

        public async Task Handle(CreateSensorReadingCommand request, CancellationToken cancellationToken)
        {
            var sensorReading = request.SensorReading;

            var key = sensorReading.DeviceKey.Trim().ToLower();

            var sensor = await _context.Sensors
                .Include(s => s.Container)
                .FirstOrDefaultAsync(
                    s => s.DeviceKey.ToLower() == key,
                    cancellationToken);

            if (sensor == null)
                throw new KeyNotFoundException($"Sensor with DeviceKey '{sensorReading.DeviceKey}' not found");

            if (sensor.Container == null || sensor.Container.DeletedAt != null)
                throw new InvalidOperationException("Sensor container is inactive or deleted");

            sensor.CurrentLidPosition = (Domain.Enums.LidPosition)sensorReading.LidPosition;

            var reading = _mapper.Map<Domain.Entities.SensorReading>(sensorReading);
            reading.SensorId = sensor.Id;
            reading.CreatedAt = DateTime.UtcNow;
            //reading.

            await _context.SensorReadings.AddAsync(reading, cancellationToken);

            await _alertService.CheckAndCreateAlertsAsync(sensor.Container, reading, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
