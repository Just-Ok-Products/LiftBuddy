using Lift.Buddy.Core.Models.Enums;

namespace Lift.Buddy.Core.Models
{
    //TODO: mettere ExerciseName, ExcerciseType in Excercise ed usare quello?
    public class PersonalRecord
    {
        public string ExerciseName { get; set; } = string.Empty;

        public int Series { get; set; }
        public int Reps { get; set; }

        public int Weight { get; set; }

        //TODO: enum al posto di string? magari un oggetto "Weight" che ha sia peso che UOM
        public string UnitOfMeasure { get; set; } = "KG";

        public ExercizeType ExerciseType { get; set; }
    }
}
