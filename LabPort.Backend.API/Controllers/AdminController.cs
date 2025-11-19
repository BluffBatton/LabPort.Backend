using LabPort.Backend.API.Common;
using LabPort.Backend.Application.Services.User.Queries;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
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

    }
}
