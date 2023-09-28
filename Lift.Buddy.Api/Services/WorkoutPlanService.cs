﻿using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
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
        private readonly IDatabaseMapper _mapper;

        public WorkoutPlanService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Get

        public async Task<Response<WorkoutPlanDTO>> GetWorkoutPlans()
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                // limitare/lazy loading
                var workoutPlan = await _context.WorkoutPlans.ToArrayAsync();

                response.Result = true;
                response.Body = workoutPlan.Select(p => _mapper.Map(p));
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlans), ex);
            }

            return response;
        }


        public async Task<Response<WorkoutPlanDTO>> GetWorkoutPlanById(Guid id)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans.SingleOrDefaultAsync(x => x.Id == id);

                if (workoutPlan == null) throw new Exception("The workplan does not exist in the database.");

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { _mapper.Map(workoutPlan) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanById), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlanDTO>> GetUserWorkoutPlans(string username)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                if (username == string.Empty) throw new Exception("No username given.");

                var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

                if (user == null) throw new Exception($"User with name '{username} doesn't exists.");

                var workoutPlans = user?.WorkoutPlans;

                response.Result = true;
                response.Body = workoutPlans?.Select(p => _mapper.Map(p));
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUserWorkoutPlans), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlanDTO>> GetWorkoutPlanCreatedByUser(string username)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                if (username == string.Empty) throw new Exception("No username given.");

                var workoutSchedules = await _context.WorkoutPlans
                    .Where(x => x.Creator.Username == username)
                    .ToArrayAsync();

                response.Body = workoutSchedules.Select(p => _mapper.Map(p));
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanCreatedByUser), ex);
            }

            return response;
        }

        public async Task<Response<int>> GetWorkoutPlanSubscribersNumber(Guid workoutPlanId)
        {
            var response = new Response<int>();
            try
            {
                var workoutPlanSubscribers = await _context.WorkoutPlans
                    .Where(x => x.Id == workoutPlanId)
                    .ToArrayAsync();

                response.Result = true;
                response.Body = new int[] { workoutPlanSubscribers.Length };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetWorkoutPlanSubscribersNumber), ex);
            }
            return response;
        }

        public async Task<Response<Document>> GetWorkoutPlanPdf(Guid workoutPlanId)
        {
            var response = new Response<Document>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .SingleOrDefaultAsync(x => x.Id == workoutPlanId);

                if (workoutPlan == null) throw new Exception("The workplan does not exist in the database.");

                var doc = _mapper.Map(workoutPlan).ToPDF();

                // TODO: servizio creazione PDF
                var pdfRenderer = new PdfDocumentRenderer(false)
                {
                    Document = doc
                };

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
        public async Task<Response<WorkoutPlanDTO>> AddWorkoutPlan(WorkoutPlanDTO workoutPlan)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                await _context.WorkoutPlans.AddAsync(_mapper.Map(workoutPlan));

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { workoutPlan };
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
        public async Task<Response<WorkoutPlanDTO>> DeleteWorkoutPlan(Guid workoutPlanId)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans.SingleOrDefaultAsync(p => p.Id == workoutPlanId);

                if (workoutPlan == null) throw new Exception("The workplan does not exist in the database.");

                _context.WorkoutPlans.Remove(workoutPlan);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new List<WorkoutPlanDTO> { _mapper.Map(workoutPlan) };
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
        public async Task<Response<WorkoutPlanDTO>> UpdateWorkoutPlan(WorkoutPlanDTO workoutPlan)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                _context.WorkoutPlans.Update(_mapper.Map(workoutPlan));

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { workoutPlan };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }

        public async Task<Response<WorkoutPlanDTO>> ReviewWorkoutPlan(Guid workoutId, int stars)
        {
            var response = new Response<WorkoutPlanDTO>();

            try
            {
                var workoutPlan = await _context.WorkoutPlans
                    .FirstOrDefaultAsync(x => x.Id == workoutId);

                if (workoutPlan == null) throw new Exception($"Trying to review non existing workout plan with id {workoutId}.");

                workoutPlan.ReviewAverage = CalculateMean(workoutPlan.ReviewAverage, workoutPlan.ReviewCount, stars);
                workoutPlan.ReviewCount++;

                _context.WorkoutPlans.Update(workoutPlan);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("Failed to save changes in database");
                }

                response.Result = true;
                response.Body = new WorkoutPlanDTO[] { _mapper.Map(workoutPlan) };
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateWorkoutPlan), ex);
            }

            return response;
        }
        #endregion

        private int CalculateMean(double currentMean, int count, double value)
            => (int)(value + (currentMean * count)) / (count + 1);
    }
}
