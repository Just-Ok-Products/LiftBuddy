using MigraDoc.DocumentObjectModel;

namespace Lift.Buddy.Core.Models
{
    public class WorkoutPlanDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ReviewsStars { get; set; } = 0;
        public int ReviewAverage { get; set; } = 0;

        public UserDTO Creator { get; set; }
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
