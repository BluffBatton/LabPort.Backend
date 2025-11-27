namespace LabPort.Backend.Contracts.DTOs.Statistics.Container
{
    public class ReadingPointDto
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
