using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class TestEntityConfiguration : BaseEntityConfiguration<Test>
    {
        public override void Configure(EntityTypeBuilder<Test> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.TestedAt)
                .IsRequired();
            builder.Property(t => t.Subject)
                .IsRequired();
            builder.Property(t => t.TestStatus)
                .IsRequired();
            builder.Property(t => t.Comment);

            builder.HasOne(t => t.Sample)
                .WithMany(s => s.Tests)
                .HasForeignKey(t => t.SampleId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
           
            builder.HasOne(tt => tt.TestType)
                .WithMany(t => t.Tests)
                .HasForeignKey(tt => tt.TestTypeId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
