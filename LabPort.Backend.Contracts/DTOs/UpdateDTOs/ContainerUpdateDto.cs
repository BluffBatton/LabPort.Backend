namespace LabPort.Backend.Contracts.DTOs.UpdateDTOs
{
    public class ContainerUpdateDto
    {
        public double? TemperatureMin { get; set; }
        public double? TemperatureMax { get; set; }
        public double? HumidityMin { get; set; }
        public double? HumidityMax { get; set; }
    }
}
