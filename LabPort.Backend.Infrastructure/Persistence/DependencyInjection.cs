using LabPort.Backend.Application.Interfaces;
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

            return services;
        }
    }
}
