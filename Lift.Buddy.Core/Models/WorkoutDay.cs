using MigraDoc.DocumentObjectModel;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.Models
{
    // giorno della settimana? non sarebbe meglio un enum invece che un int?
    // non dovrebbe essere un'entità del DBContext? magari ci vuole una mappatura di mezzo
    public class WorkoutDay
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }

        [JsonPropertyName("exercises")]
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        public Document ToPDF()
        {
            var document = new Document();

            Section section = document.AddSection();

            var paragraph = section.AddParagraph();
            foreach (var exercise in Exercises)
            {
                paragraph.AddText($"{exercise}\n");
            }

            return document;
        }
    }
}
