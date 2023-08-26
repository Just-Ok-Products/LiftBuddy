using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.DB;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.API.Services
{
    public class WorkoutScheduleService : IWorkoutScheduleService
    {
        private readonly DBContext _context;

        public WorkoutScheduleService(DBContext context)
        {
            _context = context;
        }

        #region Get
        public async Task<Response<WorkoutSchedule>> GetWorkoutSchedule(int id)
        {
            var response = new Response<WorkoutSchedule>();

            try
            {
                List<WorkoutSchedule> workoutSchedules;
                if (id > 0)
                {
                    workoutSchedules = await _context.WorkoutSchedules.Where(x => x.Id == id).ToListAsync();
                } else
                {
                    workoutSchedules = await _context.WorkoutSchedules.ToListAsync();
                }

                response.result = true;
                response.body = workoutSchedules;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(GetWorkoutSchedule), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutSchedule>> GetWorkoutScheduleByUser(string username)
        {
            var response = new Response<WorkoutSchedule>();

            try
            {
                if (username == String.Empty)
                {
                    throw new Exception("No user given.");
                }
                 
                var workoutAssignments = await _context.WorkoutAssignments
                    .Where(x => x.WorkoutUser == username)
                    .ToListAsync();

                List<WorkoutSchedule> workoutSchedules;
                foreach (var workoutAssignment in workoutAssignments)
                {
                    workoutSchedules = await _context.WorkoutSchedules
                        .Where(x => x.WorkoutAssignments.Contains(workoutAssignment))
                        .ToListAsync();

                    response.body = response.body.Concat(workoutSchedules).ToList();
                }

                response.result = true;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(GetWorkoutSchedule), ex);
            }

            return response;
        }
        #endregion

        #region Add
        public async Task<Response<WorkoutSchedule>> AddWorkoutSchedule(WorkoutSchedule schedule)
        {
            var response = new Response<WorkoutSchedule>();

            try
            {
                _context.WorkoutSchedules.Add(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }
                response.result = true;
                response.body = new List<WorkoutSchedule> { schedule };
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(AddWorkoutSchedule), ex);
            }

            return response;
        }
        #endregion

        #region Delete
        public async Task<Response<WorkoutSchedule>> DeleteWorkoutSchedule(WorkoutSchedule schedule)
        {
            var response = new Response<WorkoutSchedule>();

            try
            {
                _context.WorkoutSchedules.Remove(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.result = true;
                response.body = new List<WorkoutSchedule> { schedule };
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(DeleteWorkoutSchedule), ex);
            }

            return response;
        }
        #endregion

        #region Update
        public async Task<Response<WorkoutSchedule>> UpdateWorkoutSchedule(WorkoutSchedule schedule)
        {
            var response = new Response<WorkoutSchedule>();

            try
            {
                _context.WorkoutSchedules.Update(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.result = true;
                response.body = new List<WorkoutSchedule> { schedule };
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(UpdateWorkoutSchedule), ex);
            }

            return response;
        }
        #endregion

    }
}
