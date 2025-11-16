using LabPort.Backend.Domain.Common;
using LabPort.Backend.Domain.Enums;

namespace LabPort.Backend.Domain.Entities
{
    public class TestResult : BaseEntity
    {
        public required string Name { get; set; }
        public required DateTime PerformedAt { get; set; }
        public double? ValueNumeric { get; set; }
        public string? ValueText { get; set; }
        public string? Unit { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public string? Note { get; set; }

        public Guid TestId { get; set; }

        public virtual Test? Test { get; set; }
    }
}