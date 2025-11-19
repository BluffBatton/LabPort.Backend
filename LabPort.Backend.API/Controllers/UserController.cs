using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.User;
using LabPort.Backend.Application.Services.User.Commands;
using LabPort.Backend.Application.Services.User.Queries;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LabPort.Backend.API.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetMyProfile()
        {
            var query = new GetCurrentUserQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserUpdateDto updateDto)
        {
            var command = new UpdateUserCommand(updateDto);
            await Mediator.Send(command);
            return Ok(command);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMyPassword([FromBody] UserPasswordUpdateDto password)
        {
            var command = new UpdateUserPasswordCommand(password);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMyProfile()
        {
            var command = new DeleteUserCommand();
            await Mediator.Send(command);
            return NoContent();
        }
    }
}