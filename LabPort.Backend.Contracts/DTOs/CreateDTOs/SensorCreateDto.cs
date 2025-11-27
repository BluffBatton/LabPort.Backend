namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public class SensorCreateDto
    {
        public required string SerialName { get; set; }
        public required string DeviceKey { get; set; }
        public required Guid ContainerId { get; set; }
    }
}