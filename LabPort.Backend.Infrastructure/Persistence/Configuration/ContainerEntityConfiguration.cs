using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class ContainerEntityConfiguration : BaseEntityConfiguration<Container>
    {
        public override void Configure(EntityTypeBuilder<Container> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Label)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.TemperatureMin)
                .IsRequired();
            builder.Property(c => c.TemperatureMax)
                .IsRequired();
            builder.Property(c => c.HumidityMin)
                .IsRequired();
            builder.Property(c => c.HumidityMax)
                .IsRequired();
            
            builder.HasMany(c => c.SensorReadings)
                .WithOne(sr => sr.Container)
                .HasForeignKey(sr => sr.ContainerId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
