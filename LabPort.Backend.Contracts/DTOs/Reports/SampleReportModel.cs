namespace LabPort.Backend.Contracts.DTOs.Reports
{
    public class SampleReportModel
    {
        public Guid SampleId { get; set; }
        public required string SampleName { get; set; }
        public DateTime CollectedAt { get; set; }

        public string? SourceName { get; set; }
        public string? SourceLocation { get; set; }

        public required string UserEmail { get; set; }
        public required string UserName { get; set; }

        public required string ContainerLabel { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double HumidityMin { get; set; }
        public double HumidityMax { get; set; }

        public List<SampleReportTestResultModel> Tests { get; set; } = new();

        public required string SummaryConclusion { get; set; }
    }
}
