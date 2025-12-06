using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.Sensor.Commands;
using LabPort.Backend.Application.Services.Sensor.Queries;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    [Authorize]
    public class SensorController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateSensor(SensorCreateDto dto)
        {
            var command = new CreateSensorCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDto>> GetSensorById(Guid id)
        {
            var query = new GetSensorByIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<SensorDto>>> GetAllSensor()
        {
            var query = new GetAllSensorsQuery();
            var result = Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSensor(SensorUpdateDto dto)
        {
            var command = new UpdateSensorCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<SensorDto>> GetMySensor()
        {
            var query = new GetMySensorQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
