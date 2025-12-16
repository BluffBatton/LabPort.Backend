using LabPort.Backend.Contracts.DTOs.Enums;

namespace LabPort.Backend.Contracts.DTOs.IoT
{
    public class SetLidPositionDto
    {
        public required string DeviceKey { get; set; }
        public LidPosition LidPosition { get; set; }
    }
}