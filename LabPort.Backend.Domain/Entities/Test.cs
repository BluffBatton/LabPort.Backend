using LabPort.Backend.Domain.Common;
using LabPort.Backend.Domain.Enums;

namespace LabPort.Backend.Domain.Entities
{
    public class Test : BaseEntity
    {
        public required DateTime TestedAt { get; set; }
        public required string Subject { get; set; }
        public TestStatus TestStatus { get; set; } = TestStatus.Await;
        public string? Comment { get; set; }

        public Guid SampleId { get; set; }
        public Guid TestTypeId { get; set; }

        public required virtual Sample Sample { get; set; }
        public virtual TestType? TestType { get; set; }
        public virtual TestResult? TestResult { get; set; }
    }
}
