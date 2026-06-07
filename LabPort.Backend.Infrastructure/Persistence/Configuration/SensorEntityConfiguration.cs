using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class SensorEntityConfiguration : BaseEntityConfiguration<Sensor>
    {
        public override void Configure(EntityTypeBuilder<Sensor> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.SerialName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DeviceKey)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CurrentLidPosition)
                .IsRequired();

            builder.HasOne(x => x.Container)
                .WithOne(x => x.Sensor)
                .HasForeignKey<Sensor>(x => x.ContainerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Readings)
                .WithOne(x => x.Sensor)
                .HasForeignKey(x => x.SensorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
