using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.AuthDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Auth.Login
{
    public class RefreshTokenCommand : IRequest<AuthResponseDto>
    {
        public RefreshTokenDto RefreshTokenDto { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(ILabPortDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.RefreshTokenDto.AccessToken);
            var UserId = Guid.Parse(principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == UserId, cancellationToken);

            if (user == null ||
                user.RefreshToken != request.RefreshTokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            // Generate new tokens
            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Update user's refresh token
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

            await _context.SaveChangesAsync(cancellationToken);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                User = new UserInfoDto
                {
                    Id = user.Id,
                    Role = (Contracts.DTOs.Enums.Role)user.Role
                }
            };
        }
    }
}
