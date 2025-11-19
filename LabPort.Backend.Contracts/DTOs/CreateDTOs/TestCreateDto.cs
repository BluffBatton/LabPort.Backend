using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public class TestCreateDto
    {
        public Guid SampleId { get; set; }
        public Guid TestTypeId { get; set; }
    
        public required string Subject { get; set; }
        public DateTime? TestedAt { get; set; }
        public string? Comment { get; set; }
    }
}
