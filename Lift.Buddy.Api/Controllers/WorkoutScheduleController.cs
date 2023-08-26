using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutScheduleController : ControllerBase
    {
        private readonly IWorkoutScheduleService _workoutScheduleService;

        public WorkoutScheduleController(IWorkoutScheduleService workoutScheduleService)
        {
            _workoutScheduleService = workoutScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _workoutScheduleService.GetWorkoutSchedule(-1);
            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _workoutScheduleService.GetWorkoutSchedule(id);
            return Ok(response);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUser(string username)
        {
            var response = await _workoutScheduleService.GetWorkoutScheduleByUser(username);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkoutSchedule workoutSchedule)
        {
            var response = await _workoutScheduleService.AddWorkoutSchedule(workoutSchedule);
            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkoutSchedule workoutSchedule)
        {
            var response = await _workoutScheduleService.UpdateWorkoutSchedule(workoutSchedule);
            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
