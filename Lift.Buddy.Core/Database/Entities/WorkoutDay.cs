using MigraDoc.DocumentObjectModel;
using System.Text.Json.Serialization;
using System;

namespace Lift.Buddy.Core.Database.Entities
{
    public class WorkoutDay
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
        //TODO: spostare in DTO
        // public Document ToPDF()
        // {
        //     var document = new Document();

        //     Section section = document.AddSection();

        //     var paragraph = section.AddParagraph();
        //     foreach (var exercise in Exercises)
        //     {
        //         paragraph.AddText($"{exercise}\n");
        //     }

        //     return document;
        // }
    }
}
