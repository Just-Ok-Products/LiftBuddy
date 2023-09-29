using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models;

// QUESTION: se ho capito bene questa classe è un wrapper per un tipo ed eventuali errori.
// esiste una libreria che aggiunge Option<T> che fa la stessa cosa ma permette di fare cose
// abbastanza fighe. potrebbe valer la pena investigare 

// probabilmente è tutto risolvibile gestendo bene le eccezioni/casi di fallback
public class Response<T>
{
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    [JsonPropertyName("result")]
    public bool Result { get; set; }

    [JsonPropertyName("body")]
    public IEnumerable<T>? Body { get; set; }
}
