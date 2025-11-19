using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Infrastructure.Persistence
{
    public class LabPortDbContext : DbContext, ILabPortDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Container> Containers { get; set; }

        public DbSet<SensorReading> SensorReadings { get; set; }

        public DbSet<Sample> Samples { get; set; }

        public DbSet<Source> Sources { get; set; }

        public DbSet<SourceType> SourceTypes { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestType> TestTypes { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public LabPortDbContext(DbContextOptions<LabPortDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LabPortDbContext).Assembly);

            modelBuilder.Entity<User>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Sample>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Source>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<SourceType>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Test>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<TestType>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<TestResult>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Container>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<SensorReading>().HasQueryFilter(e => e.DeletedAt == null);

            base.OnModelCreating(modelBuilder);
        }
    }
}