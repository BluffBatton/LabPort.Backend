namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public record SensorReadingDto
    {
        public Guid Id { get; init; }
        public double Temperature { get; init; }
        public double Humidity { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
