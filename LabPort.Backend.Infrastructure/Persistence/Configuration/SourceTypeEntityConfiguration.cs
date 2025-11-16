using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class SourceTypeEntityConfiguration : BaseEntityConfiguration<SourceType>
    {
        public override void Configure(EntityTypeBuilder<SourceType> builder)
        {
            base.Configure(builder);

            builder.Property(st => st.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
