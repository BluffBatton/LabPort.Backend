using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class SensorReadingEntityConfiguration : BaseEntityConfiguration<SensorReading>
    {
        public override void Configure(EntityTypeBuilder<SensorReading> builder)
        {
            base.Configure(builder);

            builder.Property(sr => sr.Temperature)
                .IsRequired();

            builder.Property(sr => sr.Humidity)
                .IsRequired();

            builder.Property(sr => sr.LidPosition)
                .IsRequired();

            builder.Property(sr => sr.SensorId)
                .IsRequired();

            builder.HasOne(x => x.Sensor)
                .WithMany(s => s.Readings)
                .HasForeignKey(x => x.SensorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
