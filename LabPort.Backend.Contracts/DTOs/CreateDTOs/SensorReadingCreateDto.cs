using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public record SensorReadingCreateDto
    {
        public required string DeviceKey { get; set; }

        public double Temperature { get; init; }
        public double Humidity { get; init; }
        public LidPosition LidPosition { get; init; }
    }
}
