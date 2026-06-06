using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Alert.Commands
{
    public class MarkAlertAsReadCommand : IRequest<AlertDto>
    {
        public Guid AlertId { get; }

        public MarkAlertAsReadCommand(Guid alertId)
        {
            AlertId = alertId;
        }
    }

    public class MarkAlertAsReadCommandHandler : IRequestHandler<MarkAlertAsReadCommand, AlertDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public MarkAlertAsReadCommandHandler(
            ILabPortDbContext context,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<AlertDto> Handle(MarkAlertAsReadCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var alert = await _context.Alerts
                .Include(a => a.SensorReading)
                    .ThenInclude(sr => sr.Sensor)
                        .ThenInclude(s => s.Container)
                .FirstOrDefaultAsync(
                    a => a.Id == request.AlertId &&
                         a.SensorReading.Sensor.Container.UserId == userId.Value,
                    cancellationToken);

            if (alert == null)
            {
                throw new KeyNotFoundException($"Alert with id {request.AlertId} was not found or does not belong to current user.");
            }

            alert.IsRead = true;
            alert.ReadAt = DateTime.UtcNow;
            alert.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AlertDto>(alert);
        }
    }
}
