using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.DB;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.API.Services
{
    public class PRService : IPRService
    {
        private readonly DBContext _context;

        public PRService(DBContext context)
        {
            _context = context;
        }

        public async Task<Response<UserPR>> GetByUser(string username)
        {
            var response = new Response<UserPR>();

            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception("No username received");
                }

                var userPR = await _context.UserPRs.Where(x => x.Username == username).ToListAsync();

                response.body = userPR;
                response.result = true;
            }
            catch (Exception ex)
            { 
                response.notes = Utils.ErrorMessage(nameof(GetByUser), ex);
                response.result = false;
            }

            return response;
        }

        public async Task<Response<UserPR>> AddPR(UserPR userPR)
        {
            var response = new Response<UserPR>();

            try
            {
                if (userPR == null)
                {
                    throw new Exception("No data received");
                }

                _context.UserPRs.Add(userPR);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("No changes to the database done");
                }

                response.result = true;
            }
            catch (Exception ex)
            {
                response.notes = Utils.ErrorMessage(nameof(AddPR), ex);
                response.result = false;
            }

            return response;
        }

        public async Task<Response<UserPR>> UpdatePR(UserPR userPR)
        {
            var response = new Response<UserPR>();

            try
            {
                if (userPR == null)
                {
                    throw new Exception("No data received");
                }

                _context.UserPRs.Update(userPR);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("No changes to the database done");
                }

                response.result = true;
            }
            catch (Exception ex)
            {
                response.notes = Utils.ErrorMessage(nameof(UpdatePR), ex);
                response.result = false;
            }

            return response;
        }
    }
}
