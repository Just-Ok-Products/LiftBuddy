using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    public class WorkoutAssignment
    {
        [Key]
        public int WorkoutId { get; set; } = -1;

        [Key]
        public string WorkoutUser { get; set; } = "";

        [JsonIgnore, ForeignKey(nameof(WorkoutUser))]
        public virtual User User { get; set; }

        [JsonIgnore, ForeignKey(nameof(WorkoutId))]
        public virtual WorkoutPlan WorkoutSchedule { get; set; }
    }
}
