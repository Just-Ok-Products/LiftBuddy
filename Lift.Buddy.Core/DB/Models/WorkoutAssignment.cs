using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    public class WorkoutAssignment
    {
        [Key]
        public int WorkoutId { get; set; } = -1;
        [Key]
        public string WorkoutUser { get; set; } = "";

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual WorkoutSchedule WorkoutSchedule { get; set;}
    }
}
