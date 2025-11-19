namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public class SampleCreateDto
    {
        public required string Name { get; set; }
        public required DateTime CollectedAt { get; set; }
        public Guid ContainerId { get; set; }
        public Guid SourceId { get; set; }
    }
}
