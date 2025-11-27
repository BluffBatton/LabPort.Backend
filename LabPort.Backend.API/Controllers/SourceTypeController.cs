using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Source.Command;
using LabPort.Backend.Application.Services.Source.Query;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize]
    public class SourceTypeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<SourceTypeDto>>> GetAllSourceTypes()
        {
            var query = new GetSourceTypeQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}