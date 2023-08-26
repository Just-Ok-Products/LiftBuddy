using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IWorkoutScheduleService
    {
        Task<Response<WorkoutSchedule>> GetWorkoutSchedule(int id);
        Task<Response<WorkoutSchedule>> GetWorkoutScheduleByUser(string username);
        Task<Response<WorkoutSchedule>> AddWorkoutSchedule(WorkoutSchedule schedule);
        Task<Response<WorkoutSchedule>> DeleteWorkoutSchedule(WorkoutSchedule schedule);
        Task<Response<WorkoutSchedule>> UpdateWorkoutSchedule(WorkoutSchedule schedule);
    }
}
