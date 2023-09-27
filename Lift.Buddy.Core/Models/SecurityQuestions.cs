using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    // QUESTION: immagino siano le domande di sicurezza per recuper account. 
    // non sarebbe meglio associare ad ogni domanda la risposta invece che avere due liste separate?
    // forse sarebbe meglio una lista di tuple [(domanda1, risposta1),...]
    public class SecurityQuestions
    {
        [JsonPropertyName("questions")]
        public List<string> Questions { get; set; } = new List<string>();

        [JsonPropertyName("answers")]
        public List<string> Answers { get; set; } = new List<string>();
    }
}
