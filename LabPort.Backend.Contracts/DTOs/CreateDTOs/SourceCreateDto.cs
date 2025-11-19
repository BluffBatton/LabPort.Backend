namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public class SourceCreateDto
    {
        public required string Name { get; set; }
        public string? Note { get; set; }
        public string? Location { get; set; }
        public string? ContactInfo { get; set; }

        public Guid SourceTypeId { get; set; }
    }
}