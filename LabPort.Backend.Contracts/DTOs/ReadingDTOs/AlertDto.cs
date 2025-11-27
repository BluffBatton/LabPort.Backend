namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class AlertDto
    {
        public Guid Id { get; set; }

        public string Message { get; set; } = null!;
        public string? Details { get; set; }

        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public Guid SensorReadingId { get; set; }
    }
}
