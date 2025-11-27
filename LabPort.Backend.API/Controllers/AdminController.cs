using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Admin.Queries;
using LabPort.Backend.Application.Services.Source.Command;
using LabPort.Backend.Application.Services.Test.Commands;
using LabPort.Backend.Application.Services.User.Queries;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.Statistics.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var query = new GetAllUserQuery();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestType(Guid id)
        {
            var command = new DeleteTestTypeCommand(id);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSourceType(Guid Id)
        {
            var command = new DeleteSourceTypeCommand(Id);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSourceType(SourceTypeCreateDto dto)
        {
            var command = new CreateSourceTypeCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<AdminStatisticsDto>> GetStatistics([FromQuery] int days = 7)
        {
            var query = new GetAdminStatisticsQuery { Days = days };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("export/users")]
        public async Task<IActionResult> ExportUsersReport([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var csvBytes = await Mediator.Send(new ExportUsersCsvQuery(from, to));
            var fileName = $"users-report-{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
            return File(csvBytes, "text/csv", fileName);
        }

    }
}