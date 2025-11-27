using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Sample.Commands;
using LabPort.Backend.Application.Services.Sample.Queries;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize]
    public class SampleController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSample([FromBody] SampleCreateDto dto)
        {
            var command = new CreateSampleCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<SampleDto>>> GetAllSamples()
        {
            var query = new GetAllSamplesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SampleDto>> GetSampleById(Guid id)
        {
            var query = new GetSampleByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSample(Guid id, [FromBody] SampleUpdateDto dto)
        {
            var command = new UpdateSampleCommand(id, dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSample(Guid id)
        {
            var command = new DeleteSampleCommand(id);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
