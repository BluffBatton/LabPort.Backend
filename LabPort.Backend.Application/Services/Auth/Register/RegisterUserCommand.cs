using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.AuthDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Auth.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public RegisterDto Register { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Email == request.Register.User.Email, cancellationToken);

            if (userExists)
            {
                throw new Exception("User already exists");
            }

            var user = _mapper.Map<Domain.Entities.User>(request.Register.User);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Register.User.PasswordHash);
            user.CreatedAt = DateTime.UtcNow;

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var userFromDb = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Register.User.Email, cancellationToken);

            var container = _mapper.Map<Domain.Entities.Container>(request.Register.Container);
            container.UserId = userFromDb.Id;
            container.TemperatureMin = -10.00;
            container.TemperatureMax = 30.00;
            container.HumidityMin = 0;
            container.HumidityMax = 100;

            container.CreatedAt = DateTime.UtcNow;

            await _context.Containers.AddAsync(container, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
