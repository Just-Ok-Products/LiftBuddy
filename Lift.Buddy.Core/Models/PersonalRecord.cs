using Lift.Buddy.Core.Models.Enums;

namespace Lift.Buddy.Core.Models
{
    public class PersonalRecord
    {
        public string ExerciseName { get; set; } = string.Empty;

        public int Series { get; set; }
        public int Reps { get; set; }

        public int Weight { get; set; }

        public string UnitOfMeasure { get; set; } = "KG";

        public ExercizeType ExerciseType { get; set; }
    }
}
