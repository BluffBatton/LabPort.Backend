using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public class TestResultCreateDto
    {
        public Guid TestId { get; set; }
        public required string Name { get; set; }
        public DateTime? PerformedAt { get; set; }

        public double? ValueNumeric { get; set; }
        public string? ValueText { get; set; }
        public string? Unit { get; set; }

        public ResultStatus ResultStatus { get; set; }
        public string? Note { get; set; }
    }
}
