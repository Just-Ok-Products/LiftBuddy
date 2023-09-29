using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models;

public class UserDTO
{
    [JsonPropertyName("username")]
    public string Userame { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    public Credentials Credentials { get; set; }

    public IEnumerable<SecurityQuestionDTO> SecurityQuestions { get; set; } = Enumerable.Empty<SecurityQuestionDTO>();
}
