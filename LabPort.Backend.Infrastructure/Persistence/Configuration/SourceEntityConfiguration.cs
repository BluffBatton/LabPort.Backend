using LabPort.Backend.Domain.Common;
using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class SourceEntityConfiguration : BaseEntityConfiguration<Source>
    {
        public override void Configure(EntityTypeBuilder<Source> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(s => s.Note)
                .HasMaxLength(200);
            builder.Property(s => s.Location)
                .HasMaxLength(100);
            builder.Property(s => s.ContactInfo)
                .HasMaxLength(100);

            builder.HasOne(s => s.SourceType)
                .WithMany(st => st.Sources)
                .HasForeignKey(s => s.SourceTypeId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        }
    }
}
