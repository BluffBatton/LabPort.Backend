using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Admin.Commands;
using LabPort.Backend.Application.Services.Admin.Queries;
using LabPort.Backend.Application.Services.Source.Command;
using LabPort.Backend.Application.Services.Test.Commands;
using LabPort.Backend.Application.Services.User.Queries;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.Settings;
using LabPort.Backend.Contracts.DTOs.Statistics.Admin;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
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

        [HttpPatch("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUserRole(Guid id, [FromBody] UserRoleUpdateDto dto)
        {
            var command = new UpdateUserRoleCommand(id, dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserByAdminCommand(id);
            await Mediator.Send(command);
            return NoContent();
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

        [HttpPatch("{id}")]
        public async Task<ActionResult<TestTypeDto>> UpdateTestType(Guid id, [FromBody] TestTypeUpdateDto dto)
        {
            var command = new UpdateTestTypeCommand(id, dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSourceType([FromBody] SourceTypeCreateDto dto)
        {
            var command = new CreateSourceTypeCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSourceType(Guid id)
        {
            var command = new DeleteSourceTypeCommand(id);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<SourceTypeDto>> UpdateSourceType(Guid id, [FromBody] SourceTypeUpdateDto dto)
        {
            var command = new UpdateSourceTypeCommand(id, dto);
            var result = await Mediator.Send(command);
            return Ok(result);
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

        [HttpGet]
        public async Task<ActionResult<SettingsBackupDto>> ExportSettingsBackup()
        {
            var query = new ExportSettingsBackupQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SettingsBackupImportResultDto>> ImportSettingsBackup([FromBody] SettingsBackupDto dto)
        {
            var command = new ImportSettingsBackupCommand(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}