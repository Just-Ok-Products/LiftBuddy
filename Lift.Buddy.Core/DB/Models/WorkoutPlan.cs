using Lift.Buddy.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    public class WorkoutPlan
    {
        //QUESTION: non sarebbe meglio usare un guid per gli id?
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("workoutDays")]
        public List<WorkoutDay> WorkoutDays { get; set; } = new List<WorkoutDay>();

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("reviewAverage")]
        public int ReviewAverage { get; set; } = 0;

        [JsonIgnore]
        public int ReviewsAmount { get; set; } = 0;

        //QUESTION: differenza tra CreatedBy e info in Creator?
        [JsonIgnore]
        public virtual User? Creator { get; set; }

        [JsonIgnore]
        public virtual ICollection<WorkoutAssignment>? WorkoutAssignments { get; set; }

    }
}
