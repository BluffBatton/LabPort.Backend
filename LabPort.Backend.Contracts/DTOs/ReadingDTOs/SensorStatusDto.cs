namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class SensorStatusDto
    {
        public string DeviceKey { get; set; } = string.Empty;
        public string CurrentLidPosition { get; set; } = string.Empty; // Повернемо як "Open" або "Closed"
    }
}