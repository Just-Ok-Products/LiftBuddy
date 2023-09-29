using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lift.Buddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutScheduleService;

        public WorkoutPlanController(IWorkoutPlanService workoutScheduleService)
        {
            _workoutScheduleService = workoutScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var res = string.IsNullOrEmpty(username)
                ? await _workoutScheduleService.GetWorkoutPlans()
                : await _workoutScheduleService.GetUserWorkoutPlans(username);

            return Ok(res);
        }

        [HttpGet("created-by/{username}")]
        public async Task<IActionResult> GetWorkoutsCreatedBy(string username)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanCreatedByUser(username);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanById(id);
            return Ok(response);
        }

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GetWorkplanPdf(Guid id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanPdf(id);
            return Ok(response);
        }


        [HttpGet("subscribers/{id}")]
        public async Task<IActionResult> GetWorkoutPlanSubscribers(Guid id)
        {
            var response = await _workoutScheduleService.GetWorkoutPlanSubscribersNumber(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkoutPlanDTO workoutSchedule)
        {
            var response = await _workoutScheduleService.AddWorkoutPlan(workoutSchedule);
            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPut("review")]
        public async Task<IActionResult> ReviewWorkouPlan(
            [FromBody] Guid workoutPlanId,
            [FromBody] int stars
            )
        {
            var response = await _workoutScheduleService.ReviewWorkoutPlan(workoutPlanId, stars);
            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkoutPlanDTO workoutSchedule)
        {
            var response = await _workoutScheduleService.UpdateWorkoutPlan(workoutSchedule);
            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid workoutScheduleId)
        {
            var response = await _workoutScheduleService.DeleteWorkoutPlan(workoutScheduleId);
            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
