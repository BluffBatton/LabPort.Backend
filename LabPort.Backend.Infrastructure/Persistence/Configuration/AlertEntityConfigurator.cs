using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    public class AlertEntityConfigurator : BaseEntityConfiguration<Alert>
    {
        public override void Configure(EntityTypeBuilder<Alert> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Message).IsRequired();
            builder.Property(a => a.Details);
            builder.Property(a => a.IsRead);
            builder.Property(a => a.ReadAt);

            builder.HasOne(a => a.SensorReading)
                   .WithOne(r => r.Alert)
                   .HasForeignKey<Alert>(a => a.SensorReadingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
