using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IPersonalRecordService
    {
        Task<Response<PersonalRecordDTO>> GetByUsername(string username);
        Task<Response<PersonalRecordDTO>> AddPersonalRecord(PersonalRecordDTO userPR);
        Task<Response<PersonalRecordDTO>> UpdatePersonalRecord(PersonalRecordDTO userPR);
    }
}
