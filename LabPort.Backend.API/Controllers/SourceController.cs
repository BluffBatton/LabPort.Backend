using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Source.Command;
using LabPort.Backend.Application.Services.Source.Query;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize]
    public class SourceController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSource([FromBody] SourceCreateDto dto)
        {
            var command = new CreateSourceCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<SourceDto>>> GetAllSources()
        {
            var query = new GetAllSourcesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SourceDto>> GetSourceById([FromRoute] Guid id)
        {
            var query = new GetSourceByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateSource(Guid id, [FromBody] SourceUpdateDto dto)
        {
            var command = new UpdateSourceCommand(id, dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSource(Guid id)
        {
            var command = new DeleteSourceCommand(id);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
