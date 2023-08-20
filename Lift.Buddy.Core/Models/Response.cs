using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    public class Response<T>
    {
        [JsonPropertyName("notes")]
        public string? notes { get; set; }
        [JsonPropertyName("result")]
        public bool result { get; set; }
        [JsonPropertyName("body")]
        public List<T> body { get; set; } = new List<T>();
    }
}
