using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IPersonalRecordService
    {
        Task<Response<UserPersonalRecord>> GetByUser(string username);
        Task<Response<UserPersonalRecord>> AddPersonalRecord(UserPersonalRecord userPR);
        Task<Response<UserPersonalRecord>> UpdatePersonalRecord(UserPersonalRecord userPR);
    }
}
