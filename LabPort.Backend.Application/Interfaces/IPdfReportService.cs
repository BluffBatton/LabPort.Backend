using LabPort.Backend.Contracts.DTOs.Reports;

namespace LabPort.Backend.Application.Interfaces
{
    public interface IPdfReportService
    {
        byte[] GenerateSampleReportPdf(SampleReportModel model);
    }
}
