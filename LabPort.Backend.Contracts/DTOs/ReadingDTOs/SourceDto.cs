namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class SourceDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Note { get; set; }
        public string? Location { get; set; }
        public string? ContactInfo { get; set; }

        public Guid SourceTypeId { get; set; }
        public string? SourceTypeName { get; set; }
    }
}
