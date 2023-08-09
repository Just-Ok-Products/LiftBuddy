using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lift.Buddy.Core.DB.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [JsonPropertyName("username")]
        public string? UserName { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("surname")]
        public string? Surname { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; } = false;
        [JsonPropertyName("questions")]
        public string Questions { get; set; }
        [JsonPropertyName("answers")]
        public string Answers { get; set; }

    }
}
