using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Container.Commands
{
    public class UpdateContainerCommand : IRequest
    {
        public ContainerUpdateDto Container { get; set; }

        public UpdateContainerCommand(ContainerUpdateDto container)
        {
            this.Container = container;
        }
    }
    public class UpdateContainerCommandHandler : IRequestHandler<UpdateContainerCommand>
    {
        ILabPortDbContext _context;
        IUserContextService _userContextService;
        public UpdateContainerCommandHandler(
            ILabPortDbContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(UpdateContainerCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var container = await _context.Containers
                .FirstOrDefaultAsync(c => c.UserId == userId.Value, cancellationToken);

            if (container == null)
            {
                throw new Exception($"TROUBLE: Container that belongs to userId {userId.Value} doesn't exist");
            }

            var dto = request.Container;

            if (dto.TemperatureMin.HasValue)
            {
                container.TemperatureMin = dto.TemperatureMin.Value;
            }

            if (dto.TemperatureMax.HasValue)
            {
                container.TemperatureMax = dto.TemperatureMax.Value;
            }

            if (dto.HumidityMin.HasValue)
            {
                container.HumidityMin = dto.HumidityMin.Value;
            }

            if (dto.HumidityMax.HasValue)
            {
                container.HumidityMax = dto.HumidityMax.Value;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
