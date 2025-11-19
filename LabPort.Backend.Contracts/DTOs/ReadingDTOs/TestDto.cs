using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class TestDto
    {
        public Guid Id { get; set; }
        public DateTime TestedAt { get; set; }
        public required string Subject { get; set; }
        public TestStatus TestStatus { get; set; }
        public string? Comment { get; set; }

        public Guid SampleId { get; set; }
        public required string SampleName { get; set; }

        public Guid TestTypeId { get; set; }
        public required string TestTypeName { get; set; }
    }
}
