using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
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

        public async Task<Response<PersonalRecordDTO>> GetByUserId(Guid userId)
        {
            var response = new Response<PersonalRecordDTO>();

            try
            {
                var records = await _context.PersonalRecords
                    .Where(r => r.UserId == userId)
                    .Select(pr => _mapper.Map(pr))
                    .ToArrayAsync();

                response.Body = records;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Notes = Utils.ErrorMessage(nameof(GetByUserId), ex);
                response.Result = false;
            }

            return response;
        }

        public async Task<Response<PersonalRecordDTO>> UpdatePersonalRecord(
            Guid userId,
            PersonalRecords records)
        {
            var response = new Response<PersonalRecordDTO>();

            try
            {
                var toUpdate = records.ToUpdate.Select(r => _mapper.Map(r));

                var toAdd = records.ToAdd.Select(r =>
                {
                    var record = _mapper.Map(r);
                    record.UserId = userId;
                    return record;
                });

                _context.PersonalRecords.UpdateRange(toUpdate);
                await _context.PersonalRecords.AddRangeAsync(toAdd);

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
