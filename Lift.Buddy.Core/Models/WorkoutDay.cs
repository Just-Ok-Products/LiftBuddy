using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    public class WorkoutDay
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }
        [JsonPropertyName("exercises")]
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

    }
}
