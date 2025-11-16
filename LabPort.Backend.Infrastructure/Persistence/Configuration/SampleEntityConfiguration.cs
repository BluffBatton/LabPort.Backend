using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class SampleEntityConfiguration : BaseEntityConfiguration<Sample>
    {
        public override void Configure(EntityTypeBuilder<Sample> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.CollectedAt)
                .IsRequired();

            builder.HasOne(s => s.Source)
                .WithMany(src => src.Samples)
                .HasForeignKey(s => s.SourceId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasOne(c => c.Container)
                .WithMany(s => s.Samples)
                .HasForeignKey(c => c.ContainerId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
