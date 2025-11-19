namespace LabPort.Backend.Contracts.DTOs.UpdateDTOs
{
    public class SampleUpdateDto
    {
        public string? Name { get; set; }
        public DateTime? CollectedAt { get; set; }
        public Guid? SourceId { get; set; }
    }
}