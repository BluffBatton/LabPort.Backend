using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.User.Commands
{
    public class UpdateUserRoleCommand : IRequest
    {
        public Guid UserId { get; set; }

        public UpdateUserRoleCommand(Guid userId)
        {
            UserId = userId;
        }
    }

    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly ILabPortDbContext _context;

        public UpdateUserRoleCommandHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new Exception($"User with Id {request.UserId} was not found");
            }


        }
    }
}
