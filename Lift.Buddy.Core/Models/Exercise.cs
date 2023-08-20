using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    public class Exercise
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("reps")]
        public int? Repetitions { get; set; }
        [JsonPropertyName("series")]
        public int? Series { get; set; }
        [JsonPropertyName("time")]
        public DateTime? Time { get; set; }
        [JsonPropertyName("rest")]
        public DateTime? Rest { get; set; }

        public Exercise() { }

    }
}
