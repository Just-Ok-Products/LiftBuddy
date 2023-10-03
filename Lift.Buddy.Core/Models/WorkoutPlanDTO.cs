using MigraDoc.DocumentObjectModel;

namespace Lift.Buddy.Core.Models
{
    public class WorkoutPlanDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int ReviewsCount { get; set; } = 0;
        public float ReviewAverage { get; set; } = 0;
        public Guid CreatorId { get; set; }
        public IEnumerable<WorkoutDayDTO> WorkoutDays { get; set; } = Enumerable.Empty<WorkoutDayDTO>();

        public Document ToPDF()
        {
            var document = new Document
            {
                UseCmykColor = true
            };

            var section = document.AddSection();
            var paragraph = section.AddParagraph();

            foreach (var day in WorkoutDays)
            {
                paragraph.AddText($"\n{day.Day}\n\n");
                foreach (var exercise in day.Exercises)
                {
                    paragraph.AddText($"{exercise}\n");
                }
            }

            return document;
        }
    }
}
