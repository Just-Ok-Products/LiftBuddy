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

        public async Task<Response<PersonalRecordDTO>> GetByUsername(string username)
        {
            var response = new Response<PersonalRecordDTO>();

            try
            {
                if (string.IsNullOrEmpty(username)) throw new Exception("No username received");

                var user = await _context.Users
                    .SingleOrDefaultAsync(x => x.Username == username);


                response.Body = user?.PersonalRecords
                    .Select(pr => _mapper.Map(pr)) ?? Enumerable.Empty<PersonalRecordDTO>();

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Notes = Utils.ErrorMessage(nameof(GetByUsername), ex);
                response.Result = false;
            }

            return response;
        }

        public async Task<Response<PersonalRecordDTO>> AddPersonalRecord(PersonalRecordDTO record)
        {
            var response = new Response<PersonalRecordDTO>();

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

        public async Task<Response<PersonalRecordDTO>> UpdatePersonalRecord(PersonalRecordDTO record)
        {
            var response = new Response<PersonalRecordDTO>();

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
