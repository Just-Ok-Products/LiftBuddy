using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IPRService
    {
        Task<Response<UserPR>> GetByUser(string username);

        Task<Response<UserPR>> AddPR(UserPR userPR);
        Task<Response<UserPR>> UpdatePR(UserPR userPR);
    }
}
