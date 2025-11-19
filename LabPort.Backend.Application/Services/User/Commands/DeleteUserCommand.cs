using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.User.Commands
{
    public class DeleteUserCommand : IRequest
    {

    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand> 
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;
        public DeleteUserCommandHandler(ILabPortDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if(userId == null)
            {
                throw new Exception("User is not authenticated");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);
            if (user == null) 
            {
                throw new Exception($"TROUBLE: User with Id {userId} was not found in database");
            }

            user.DeletedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow; // Should service update this field
                                              // in case of soft deleting user?
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}