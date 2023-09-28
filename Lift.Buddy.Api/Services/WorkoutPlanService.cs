﻿using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Text;

namespace Lift.Buddy.API.Services
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly LiftBuddyContext _context;

        public WorkoutPlanService(LiftBuddyContext context)
        {
            _context = context;
        }

        #region Get

        public async Task<Response<WorkoutPlan>> GetWorkoutPlanById(int id)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                // QUESTION: se ho capito bene, dando un ID > 0 si ritorna una lista con un solo risultato,
                // altrimenti tutti i workout. corretto?
                List<WorkoutPlan> workoutSchedules;
                if (id > 0)
                {
                    workoutSchedules = await _context.WorkoutPlans.Where(x => x.Id == id).ToListAsync();
                }
                else
                {
                    workoutSchedules = await _context.WorkoutPlans.ToListAsync();
                }

                response.Result = true;
                response.Body = workoutSchedules;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlan>> GetWorkoutPlanAssignedToUsername(string username)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                if (username == string.Empty) throw new Exception("No username given.");

                // a naso questo potrebbe essere migliorato facendo una sola query e ristrutturando
                // le references tra gli oggetti su db
                var workoutAssignments = await _context.WorkoutAssignments
                    .Where(x => x.WorkoutUser == username)
                    .ToListAsync();

                List<WorkoutPlan> workoutSchedules;
                foreach (var workoutAssignment in workoutAssignments)
                {
                    workoutSchedules = await _context.WorkoutPlans
                        .Where(x => x.WorkoutAssignments.Contains(workoutAssignment))
                        .ToListAsync();

                    response.Body = response.Body.Concat(workoutSchedules).ToList();
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlan>> GetWorkoutPlanCreatedByUsername(string username)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                if (username == string.Empty) throw new Exception("No username given.");

                var workoutSchedules = await _context.WorkoutPlans
                    .Where(x => x.CreatedBy == username)
                    .ToListAsync();

                response.Body = workoutSchedules;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<int>> GetWorkoutPlanSubscribersNumber(int workoutPlanId)
        {
            var response = new Response<int>();
            try
            {
                var workoutPlanSubscribers = await _context.WorkoutAssignments
                    .Where(x => x.WorkoutId == workoutPlanId)
                    .ToListAsync();

                response.Result = true;
                response.Body = new List<int>
                {
                    workoutPlanSubscribers.Count
                };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanSubscribersNumber), ex);
            }
            return response;
        }

        public async Task<Response<Document>> GetWorkoutPlanPdf(int workplanId)
        {
            var response = new Response<Document>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .Where(x => x.Id == workplanId)
                    .FirstOrDefaultAsync();

                if (workoutPlan == null) throw new Exception("The workplan does not exist in the database.");

                var doc = workoutPlan.WorkoutDays[0].ToPDF();
                doc.UseCmykColor = true;

                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false)
                {
                    Document = doc
                };

                // corretto qua o dovrebbe essere registrato con il resto dei servizi?
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                pdfRenderer.RenderDocument();

                const string filename = "HelloWorld.pdf";
                pdfRenderer.PdfDocument.Save(filename);
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanPdf), ex);
            }

            return response;
        }
        #endregion

        #region Add
        public async Task<Response<WorkoutPlan>> AddWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                _context.WorkoutPlans.Add(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(AddWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

        #region Delete
        public async Task<Response<WorkoutPlan>> DeleteWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                _context.WorkoutPlans.Remove(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(DeleteWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

        #region Update
        public async Task<Response<WorkoutPlan>> UpdateWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                _context.WorkoutPlans.Update(schedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlan>> ReviewWorkoutPlan(WorkoutPlan schedule)
        {
            var response = new Response<WorkoutPlan>();

            try
            {
                if (schedule == null) throw new Exception("Cannot review with empty data.");

                var oldSchedule = await _context.WorkoutPlans
                    .FirstOrDefaultAsync(x => x.Id == schedule.Id);

                if (oldSchedule == null) throw new Exception($"Trying to review non existing workout plan with id {schedule.Id}.");

                oldSchedule.ReviewAverage = (schedule.ReviewAverage + (oldSchedule.ReviewAverage * oldSchedule.ReviewsStars)) / (oldSchedule.ReviewsStars + 1);
                oldSchedule.ReviewsStars++;

                _context.WorkoutPlans.Update(oldSchedule);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new List<WorkoutPlan> { schedule };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

    }
}
