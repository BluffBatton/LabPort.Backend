namespace LabPort.Backend.Contracts.DTOs.ReadingDTOs
{
    public class ContainerDto
    {
        public Guid Id { get; set; }
        public required string Label { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double HumidityMin { get; set; }
        public double HumidityMax { get; set; }
    }
}
