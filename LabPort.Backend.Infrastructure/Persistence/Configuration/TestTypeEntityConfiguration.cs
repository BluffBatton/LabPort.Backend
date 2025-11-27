using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class TestTypeEntityConfiguration : BaseEntityConfiguration<TestType>
    {
        public override void Configure(EntityTypeBuilder<TestType> builder)
        {
            base.Configure(builder);

            builder.Property(tt => tt.Name)
                .IsRequired();

            builder.Property(tt => tt.ReferenceMin);
            builder.Property(tt => tt.ReferenceMax);
            builder.Property(tt => tt.Unit);
        }
    }
}
