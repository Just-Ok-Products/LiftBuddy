using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models;

public class Credentials
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = "";

    // controlli particolari sulla password? lunghezza, caratteri ecc? o da fare sul frontend?
    [JsonPropertyName("password")]
    public string Password { get; set; } = "";

    public bool HasValues()
        => !string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password);
}
