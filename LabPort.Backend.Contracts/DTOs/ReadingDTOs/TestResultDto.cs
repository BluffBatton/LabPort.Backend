using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class TestResultDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
        public DateTime PerformedAt { get; set; }

        public double? ValueNumeric { get; set; }
        public string? ValueText { get; set; }
        public string? Unit { get; set; }

        public ResultStatus ResultStatus { get; set; }
        public string? Note { get; set; }

        public Guid TestId { get; set; }
        public required string TestSubject { get; set; }
    }
}