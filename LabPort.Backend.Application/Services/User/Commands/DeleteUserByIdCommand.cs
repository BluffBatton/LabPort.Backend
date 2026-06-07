using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.User.Commands
{
    public class DeleteUserByIdCommand : IRequest
    {
        public Guid UserId { get; set; }

        public DeleteUserByIdCommand(Guid userId)
        {
            UserId = userId;
        }
    }

    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand>
    {
        private readonly ILabPortDbContext _context;

        public DeleteUserByIdCommandHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User with Id {request.UserId} doesn't exists");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
