using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IPersonalRecordService
    {
        Task<Response<PersonalRecord>> GetByUsername(string username);
        Task<Response<PersonalRecord>> AddPersonalRecord(PersonalRecordDTO userPR);
        Task<Response<PersonalRecord>> UpdatePersonalRecord(PersonalRecordDTO userPR);
    }
}
