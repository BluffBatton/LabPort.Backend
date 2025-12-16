using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class SensorDto
    {
        public Guid Id { get; set; }
        public required string SerialName { get; set; }
        public required string DeviceKey { get; set; }
        public LidPosition CurrentLidPosition { get; set; }
    }
}
