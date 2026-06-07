namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class SampleDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required DateTime CollectedAt { get; set; }

        public Guid ContainerId { get; set; }
        public required string ContainerOwnerFullName { get; set; }

        public Guid SourceId { get; set; }
        public required string SourceName { get; set; }

        public string? SourceTypeName { get; set; }
    }
}
