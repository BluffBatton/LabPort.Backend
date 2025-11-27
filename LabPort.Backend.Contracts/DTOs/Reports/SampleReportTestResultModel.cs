namespace LabPort.Backend.Contracts.DTOs.Reports
{
    public class SampleReportTestResultModel
    {
        public required string TestTypeName { get; set; }
        public DateTime TestedAt { get; set; }
        public string? ResultName { get; set; }
        public double? ValueNumeric { get; set; }
        public string? Unit { get; set; }
        public required string ResultStatus { get; set; }
        public string? Note { get; set; }
    }
}
