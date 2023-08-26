using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PRController : ControllerBase
    {
        private readonly IPRService _prService;
        public PRController(IPRService prSservice) 
        {
            _prService = prSservice;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var response = await _prService.GetByUser(username);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserPR userPR)
        {
            var response = await _prService.AddPR(userPR);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserPR userPR)
        {
            var response = await _prService.UpdatePR(userPR);
            return Ok(response);
        }
    }
}
