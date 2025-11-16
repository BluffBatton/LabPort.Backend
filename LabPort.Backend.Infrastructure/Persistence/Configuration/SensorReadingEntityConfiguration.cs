using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class SensorReadingEntityConfiguration : BaseEntityConfiguration<SensorReading>
    {
        public override void Configure(EntityTypeBuilder<SensorReading> builder)
        {
            base.Configure(builder);

            builder.Property(sr => sr.Temperature);
            builder.Property(sr => sr.Humidity);
            builder.Property(sr => sr.LidPosition);
        }
    }
}