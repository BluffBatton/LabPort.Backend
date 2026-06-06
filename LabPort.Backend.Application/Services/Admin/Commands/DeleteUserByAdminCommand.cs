using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Admin.Commands
{
    public class DeleteUserByAdminCommand : IRequest
    {
        public Guid UserId { get; }

        public DeleteUserByAdminCommand(Guid userId)
        {
            UserId = userId;
        }
    }

    public class DeleteUserByAdminCommandHandler : IRequestHandler<DeleteUserByAdminCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public DeleteUserByAdminCommandHandler(ILabPortDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(DeleteUserByAdminCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            if (currentUserId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            if (request.UserId == currentUserId.Value)
            {
                throw new InvalidOperationException("Admin cannot delete their own account.");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {request.UserId} was not found.");
            }

            user.DeletedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
