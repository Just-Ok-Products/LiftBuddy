using Lift.Buddy.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    public class WorkoutSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("workoutDays")]
        public List<WorkoutDay> WorkoutDays { get; set; } = new List<WorkoutDay>();

        [JsonIgnore]
        public virtual ICollection<WorkoutAssignment>? WorkoutAssignments { get; set; }
        
    }
}
