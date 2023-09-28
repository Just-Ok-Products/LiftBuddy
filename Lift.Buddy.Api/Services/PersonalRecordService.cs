using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.API.Services
{
    public class PersonalRecordService : IPersonalRecordService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;

        public PersonalRecordService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<PersonalRecord>> GetByUsername(string username)
        {
            var response = new Response<PersonalRecord>();

            try
            {
                if (string.IsNullOrEmpty(username)) throw new Exception("No username received");

                //var personalRecord = await _context.UserPRs.Where(x => x.Username == username).ToListAsync();
                var personalRecords = (await _context.Users.SingleOrDefaultAsync(x => x.Username == username))?
                    .PersonalRecords;
                //.Select(pr => pr.ToDTO());

                response.Body = personalRecords?.ToArray() ?? Enumerable.Empty<PersonalRecord>();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Notes = Utils.ErrorMessage(nameof(GetByUsername), ex);
                response.Result = false;
            }

            return response;
        }

        public async Task<Response<PersonalRecord>> AddPersonalRecord(PersonalRecordDTO record)
        {
            var response = new Response<PersonalRecord>();

            try
            {
                if (record == null) throw new Exception("No data received");

                var personalRecord = _mapper.Map(record);

                await _context.PersonalRecords.AddAsync(personalRecord);

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

        public async Task<Response<PersonalRecord>> UpdatePersonalRecord(PersonalRecordDTO record)
        {
            var response = new Response<PersonalRecord>();

            try
            {
                if (record == null) throw new Exception("No data received");

                var personalRecord = _mapper.Map(record);

                _context.PersonalRecords.Update(personalRecord);

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
