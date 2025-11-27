using LabPort.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Interfaces
{
    public interface ILabPortDbContext
    {
        DbSet<Alert> Alerts { get; }
        DbSet<User> Users { get; }
        DbSet<Container> Containers { get; }
        DbSet<Sensor> Sensors { get; }
        DbSet<SensorReading> SensorReadings { get; }
        DbSet<Sample> Samples { get; }  
        DbSet<Source> Sources { get; }
        DbSet<SourceType> SourceTypes { get; }
        DbSet<Test> Tests { get; }
        DbSet<TestType> TestTypes { get; }
        DbSet<TestResult> TestResults { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
