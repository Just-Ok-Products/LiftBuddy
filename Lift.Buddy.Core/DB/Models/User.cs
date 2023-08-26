using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsTrainer { get; set; } = false;
        public string Questions { get; set; }
        public string Answers { get; set; }

        [JsonIgnore]
        public virtual ICollection<WorkoutAssignment>? WorkoutAssignments { get; set; }
        [JsonIgnore]
        public virtual UserPR UserPR { get; set; }
    }
}
