using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sensor.Commands
{
    public class CreateSensorCommand : IRequest
    {
        public SensorCreateDto Sensor { get; set; }
        public CreateSensorCommand(SensorCreateDto sensor) 
        {
            Sensor = sensor;
        }
    }

    public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CreateSensorCommandHandler(
            ILabPortDbContext context,
            IMapper mapper,
            IUserContextService userContextService
            )
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task Handle(CreateSensorCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Sensor;

            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var container = await _context.Containers.FirstOrDefaultAsync(c => c.UserId == userId.Value, cancellationToken);

            if (container.UserId != userId.Value)
            {
                throw new UnauthorizedAccessException("You cannot create sensor for another user's container");
            }

            var existingSensor = await _context.Sensors
                .AnyAsync(s => s.ContainerId == container.Id, cancellationToken);

            if (existingSensor)
            {
                throw new Exception("Sensor for this container already exists");
            }

            var sensor = _mapper.Map<Domain.Entities.Sensor>(dto);
            sensor.ContainerId = container.Id;
            sensor.CreatedAt = DateTime.UtcNow;

            await _context.Sensors.AddAsync(sensor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
