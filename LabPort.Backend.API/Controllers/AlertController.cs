using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Alert.Commands;
using LabPort.Backend.Application.Services.Alert.Queries;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize]
    public class AlertController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<AlertDto>>> GetAllAlerts()
        {
            var query = new GetAllAlertsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AlertDto>> GetAlertById(Guid id)
        {
            var query = new GetAlertByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<AlertDto>> MarkAsRead(Guid id)
        {
            var command = new MarkAlertAsReadCommand(id);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(Guid id)
        {
            var command = new DeleteAlertCommand(id);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
