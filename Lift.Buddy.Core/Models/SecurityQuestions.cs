﻿using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    public class SecurityQuestions
    {
        [JsonPropertyName("questions")]
        public List<string> Questions { get; set; } = new List<string>();
        [JsonPropertyName("answers")]
        public List<string> Answers { get; set; } = new List<string>();
    }
}