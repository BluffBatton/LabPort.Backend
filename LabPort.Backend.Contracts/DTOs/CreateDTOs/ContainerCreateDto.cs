namespace LabPort.Backend.Contracts.DTOs.CreateDTOs
{
    public class ContainerCreateDto
    {
        public required string Label { get; set; } = "User Container";
        public required double TemperatureMin { get; set; }
        public required double TemperatureMax { get; set; }
        public required double HumidityMin { get; set; }
        public required double HumidityMax { get; set; }
        public Guid UserId { get; set; }
    }
}