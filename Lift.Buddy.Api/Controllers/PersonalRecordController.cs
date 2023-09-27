using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonalRecordController : ControllerBase
    {
        private readonly IPersonalRecordService _recordService;

        public PersonalRecordController(IPersonalRecordService prSservice)
        {
            _recordService = prSservice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var response = await _recordService.GetByUser(username);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserPersonalRecord userRecord)
        {
            var response = await _recordService.AddPersonalRecord(userRecord);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserPersonalRecord userRecord)
        {
            var response = await _recordService.UpdatePersonalRecord(userRecord);
            return Ok(response);
        }
    }
}
