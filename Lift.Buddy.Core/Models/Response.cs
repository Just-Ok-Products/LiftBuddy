using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    // QUESTION: se ho capito bene questa classe è un wrapper per un tipo ed eventuali errori.
    // esiste una libreria che aggiunge Option<T> che fa la stessa cosa ma permette di fare cose
    // abbastanza fighe. potrebbe valer la pena investigare 

    // probabilmente è tutto risolvibile gestendo bene le eccezioni/casi di fallback
    public class Response<T>
    {
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        // QUESTION: penso di non aver capito lo scopo del campo
        [JsonPropertyName("result")]
        public bool Result { get; set; }

        //QUESTION: list o IEnumerable? deve essere modificata? da fare check
        [JsonPropertyName("body")]
        public List<T> Body { get; set; } = new List<T>();
    }
}
