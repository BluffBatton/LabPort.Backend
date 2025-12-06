using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Admin.Queries;
using LabPort.Backend.Application.Services.Container.Queries;
using LabPort.Backend.Application.Services.Sample.Queries;
using LabPort.Backend.Contracts.DTOs.Statistics.Admin;
using LabPort.Backend.Contracts.DTOs.Statistics.Container;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    public class StatisticsController : BaseController
    {
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<ContainerReadingStatsDto>> GetContainerReadingsStats(
            [FromQuery] string range = "last7days")
        {
            var query = new GetContainerReadingsStatsQuery { Range = range };

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{sampleId}/report")]
        public async Task<IActionResult> GetSampleReportPdf(Guid sampleId)
        {
            var pdfBytes = await Mediator.Send(new GetSampleReportPdfQuery(sampleId));
            return File(pdfBytes, "application/pdf", $"Sample_{sampleId}.pdf");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminUserReportItemDto>> GetUserInfoStatistics(Guid id)
        {
            var query = new GetUserItemStatisticsQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
