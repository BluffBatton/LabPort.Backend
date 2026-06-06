using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Admin.Commands
{
    public class UpdateUserRoleCommand : IRequest<UserDto>
    {
        public Guid UserId { get; }
        public UserRoleUpdateDto Dto { get; }

        public UpdateUserRoleCommand(Guid userId, UserRoleUpdateDto dto)
        {
            UserId = userId;
            Dto = dto;
        }
    }

    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, UserDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public UpdateUserRoleCommandHandler(ILabPortDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<UserDto> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto == null)
            {
                throw new InvalidOperationException("User role update body is required.");
            }

            if (!Enum.IsDefined(typeof(Contracts.DTOs.Enums.Role), request.Dto.Role))
            {
                throw new InvalidOperationException("Role must be User or Admin.");
            }

            var currentUserId = _userContextService.GetCurrentUserId();
            if (currentUserId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {request.UserId} was not found.");
            }

            var requestedRole = (Domain.Enums.Role)request.Dto.Role;
            if (user.Id == currentUserId.Value &&
                user.Role == Domain.Enums.Role.Admin &&
                requestedRole != Domain.Enums.Role.Admin)
            {
                throw new InvalidOperationException("Admin cannot remove their own Admin role.");
            }

            user.Role = requestedRole;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = (Contracts.DTOs.Enums.Role)user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                LastLoginAt = user.LastLoginAt
            };
        }
    }
}
