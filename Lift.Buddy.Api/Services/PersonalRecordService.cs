using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.DB;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.API.Services
{
    public class PersonalRecordService : IPersonalRecordService
    {
        private readonly DBContext _context;

        public PersonalRecordService(DBContext context)
        {
            _context = context;
        }

        public async Task<Response<UserPersonalRecord>> GetByUser(string username)
        {
            var response = new Response<UserPersonalRecord>();

            try
            {
                if (string.IsNullOrEmpty(username)) throw new Exception("No username received");

                var userPR = await _context.UserPRs.Where(x => x.Username == username).ToListAsync();

                response.Body = userPR;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Notes = Utils.ErrorMessage(nameof(GetByUser), ex);
                response.Result = false;
            }

            return response;
        }

        public async Task<Response<UserPersonalRecord>> AddPersonalRecord(UserPersonalRecord userPR)
        {
            var response = new Response<UserPersonalRecord>();

            try
            {
                if (userPR == null) throw new Exception("No data received");

                _context.UserPRs.Add(userPR);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("No changes to the database done");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Notes = Utils.ErrorMessage(nameof(AddPersonalRecord), ex);
                response.Result = false;
            }

            return response;
        }

        public async Task<Response<UserPersonalRecord>> UpdatePersonalRecord(UserPersonalRecord userPR)
        {
            var response = new Response<UserPersonalRecord>();

            try
            {
                if (userPR == null) throw new Exception("No data received");

                _context.UserPRs.Update(userPR);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception("No changes to the database done");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Notes = Utils.ErrorMessage(nameof(UpdatePersonalRecord), ex);
                response.Result = false;
            }

            return response;
        }
    }
}
