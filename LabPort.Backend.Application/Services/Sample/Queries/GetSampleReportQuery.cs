using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Reports;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sample.Queries
{
    public record GetSampleReportPdfQuery(Guid SampleId) : IRequest<byte[]>;

    public class GetSampleReportPdfQueryHandler : IRequestHandler<GetSampleReportPdfQuery, byte[]>
    {
        private readonly ILabPortDbContext _context;
        private readonly IPdfReportService _pdfService;

        public GetSampleReportPdfQueryHandler(ILabPortDbContext context, IPdfReportService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        public async Task<byte[]> Handle(GetSampleReportPdfQuery request, CancellationToken ct)
        {
            var model = await _context.Samples
                .AsNoTracking()
                .Where(s => s.Id == request.SampleId)
                .Select(s => new SampleReportModel
                {
                    SampleId = s.Id,
                    SampleName = s.Name,
                    CollectedAt = s.CollectedAt,
                    SourceName = s.Source != null ? s.Source.Name : "—",
                    SourceLocation = s.Source != null ? s.Source.Location : "—",
                    UserEmail = s.Container.User != null ? s.Container.User.Email : "—",
                    UserName = s.Container.User != null
                        ? (s.Container.User.FirstName + " " + s.Container.User.LastName).Trim()
                        : "—",
                    ContainerLabel = s.Container != null ? s.Container.Label : "—",
                    TemperatureMin = s.Container != null ? s.Container.TemperatureMin : 0,
                    TemperatureMax = s.Container != null ? s.Container.TemperatureMax : 0,
                    HumidityMin = s.Container != null ? s.Container.HumidityMin : 0,
                    HumidityMax = s.Container != null ? s.Container.HumidityMax : 0,
                    SummaryConclusion = "—"
                })
                .FirstOrDefaultAsync(ct);

            if (model == null)
                throw new KeyNotFoundException($"Sample {request.SampleId} not found.");

            return _pdfService.GenerateSampleReportPdf(model);
        }
    }
}
