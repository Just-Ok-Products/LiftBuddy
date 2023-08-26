using Lift.Buddy.Core.Models;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    public class UserPR
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        [JsonPropertyName("personalRecords")]
        public List<PersonalRecord> PersonalRecords { get; set; } = new List<PersonalRecord>();
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
