using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.UpdateDTOs
{
    public class TestUpdatedDto
    {
        public TestStatus? TestStatus { get; set; }
        public string? Comment { get; set; }
        public Guid? TestTypeId { get; set; }
    }
}
