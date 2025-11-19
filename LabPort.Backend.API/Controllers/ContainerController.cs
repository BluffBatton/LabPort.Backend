using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Container.Commands;
using LabPort.Backend.Application.Services.Container.Queries;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize(Roles = "User")]
    public class ContainerController : BaseController
    {
        [HttpPatch]
        public async Task<IActionResult> UpdateContainer([FromBody] ContainerUpdateDto dto)
        {
            var command = new UpdateContainerCommand(dto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<ContainerDto>> GetContainer()
        {
            var query = new GetContainerQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
