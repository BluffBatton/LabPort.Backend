using LabPort.Backend.Domain.Entities;
using LabPort.Backend.Infrastructure.Persistence.Common;

namespace LabPort.Backend.Infrastructure.Persistence.Configuration
{
    internal class TestResultEntityConfiguration : BaseEntityConfiguration<TestResult>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TestResult> builder)
        {
            base.Configure(builder);

            builder.Property(tr => tr.Name)
                .IsRequired();
            builder.Property(tr => tr.ValueNumeric);
            builder.Property(tr => tr.ValueText);
            builder.Property(tr => tr.Unit);
            builder.Property(tr => tr.ResultStatus);
            builder.Property(tr => tr.Note);
            builder.Property(tr => tr.PerformedAt);

            builder.HasOne(tr => tr.Test)
                .WithOne(t => t.TestResult)
                .HasForeignKey<TestResult>(t => t.TestId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
