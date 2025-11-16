using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Infrastructure.Integration.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LabPort.Backend.Infrastructure.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
