using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface IPersonalRecordService
    {
        Task<Response<PersonalRecordDTO>> GetByUserId(Guid userId);
        Task<Response<PersonalRecordDTO>> AddPersonalRecord(Guid userId, IEnumerable<PersonalRecordDTO> personalRecords);
        Task<Response<PersonalRecordDTO>> UpdatePersonalRecord(Guid userId, IEnumerable<PersonalRecordDTO> personalRecords);
    }
}
