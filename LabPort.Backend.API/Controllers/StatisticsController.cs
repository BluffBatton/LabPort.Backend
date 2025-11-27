using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Container.Queries;
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
    }
}
