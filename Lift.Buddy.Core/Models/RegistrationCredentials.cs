using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lift.Buddy.Core.Models
{
    public class RegistrationCredentials: UserData
    {
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("questions")]
        public List<string> Questions { get; set; } = new List<string>();
        [JsonPropertyName("answers")]
        public List<string> Answers { get; set; } = new List<string>();
    }
}
