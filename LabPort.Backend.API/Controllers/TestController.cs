using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Test.Commands;
using LabPort.Backend.Application.Services.Test.Queries;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize]
    public class TestController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] TestCreateDto dto)
        {
            var command = new CreateTestCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTest(Guid id, [FromBody] TestUpdatedDto dto)
        {
            var command = new UpdateTestCommand(id, dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestDto>> GetTestById(Guid id)
        {
            var query = new GetTestByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<TestDto>>> GetAllTests()
        {
            var query = new GetAllTestsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestType([FromBody] TestTypeCreateDto dto)
        {
            var command = new CreateTestTypeCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<TestTypeDto>>> GetTestTypeQuery()
        {
            var query = new GetTestTypeQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestType(Guid id)
        {
            var command = new DeleteTestTypeCommand(id);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
