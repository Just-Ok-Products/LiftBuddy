using System.Security.Claims;
using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return NotFound();

            var response = await _recordService.GetByUserId(Guid.Parse(userId));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IEnumerable<PersonalRecordDTO> userRecord)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return NotFound();

            var response = await _recordService.AddPersonalRecord(Guid.Parse(userId), userRecord);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IEnumerable<PersonalRecordDTO> userRecords)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return NotFound();

            var response = await _recordService.UpdatePersonalRecord(Guid.Parse(userId), userRecords);
            return Ok(response);
        }
    }
}
