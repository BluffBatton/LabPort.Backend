using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.TestResult.Commands;
using LabPort.Backend.Application.Services.TestResult.Queries;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize]
    public class TestResultController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateTestResult([FromBody] TestResultCreateDto dto)
        {
            var command = new CreateTestResultCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<TestResultDto>>> GetAllTestResults()
        {
            var query = new GetAllTestResultsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestResultDto>> GetTestResultById(Guid id)
        {
            var query = new GetTestResultByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
