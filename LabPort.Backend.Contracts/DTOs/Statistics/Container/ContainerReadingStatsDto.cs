namespace LabPort.Backend.Contracts.DTOs.Statistics.Container
{
    public class ContainerReadingStatsDto
    {
        public Guid ContainerId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public double? TempMin { get; set; }
        public double? TempMax { get; set; }
        public double? TempAvg { get; set; }

        public double? HumMin { get; set; }
        public double? HumMax { get; set; }
        public double? HumAvg { get; set; }

        public List<ReadingPointDto> Points { get; set; } = new();
    }
}
