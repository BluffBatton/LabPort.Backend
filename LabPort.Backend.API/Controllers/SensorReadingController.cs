using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.SensorReading.Commands;
using LabPort.Backend.Application.Services.SensorReading.Queries;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Controllers
{
    //[Authorize]
    public class SensorReadingController : BaseController
    {
        [HttpGet("{take}")]
        public async Task<ActionResult<List<SensorReadingDto>>> GetAllSensorReadings(int take)
        {
            var query = new GetAllSensorReadingsQuery(take);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<SensorReadingDto>> GetSensorReadingById(Guid Id)
        {
            var query = new GetSensorReadingByIdQuery(Id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSensor(SensorReadingCreateDto dto)
        {
            var command = new CreateSensorReadingCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
