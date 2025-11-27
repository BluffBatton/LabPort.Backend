using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.AuthDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Auth.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public required RegisterDto Register { get; set; }
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
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Register == null)
            {
                throw new ArgumentNullException(nameof(request.Register), "Register payload is required.");
            }

            if (request.Register.User == null)
            {
                throw new ArgumentNullException(nameof(request.Register.User), "User object is required.");
            }
                

            var userDto = request.Register.User;
            var containerDto = request.Register.Container; 

            var userExists = await _context.Users
                .AnyAsync(u => u.Email == userDto.Email, cancellationToken);

            if (userExists)
            {
                throw new Exception("User already exists");
            }
                
            var user = _mapper.Map<Domain.Entities.User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash);
            user.CreatedAt = DateTime.UtcNow;

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
 
            Domain.Entities.Container container;

            if (containerDto != null)
            {
                container = _mapper.Map<Domain.Entities.Container>(containerDto)
                           ?? new Domain.Entities.Container { Label = containerDto.Label };
            }
            else
            {
                container = new Domain.Entities.Container { Label = "User Container" };
            }

            container.UserId = user.Id;
            container.Label ??= "User Container";
            container.TemperatureMin = container.TemperatureMin == default ? -10.0 : container.TemperatureMin;
            container.TemperatureMax = container.TemperatureMax == default ? 30.0 : container.TemperatureMax;
            container.HumidityMin = container.HumidityMin == default ? 0 : container.HumidityMin;
            container.HumidityMax = container.HumidityMax == default ? 100 : container.HumidityMax;
            container.CreatedAt = DateTime.UtcNow;

            await _context.Containers.AddAsync(container, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

    }
}
