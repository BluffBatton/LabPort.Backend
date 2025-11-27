using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LabPort.Backend.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LabPortDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<ILabPortDbContext>(provider =>
            provider.GetRequiredService<LabPortDbContext>());
            services.AddScoped<ISensorReadingCleanupService, SensorReadingCleanupService>();

            return services;
        }
    }
}
