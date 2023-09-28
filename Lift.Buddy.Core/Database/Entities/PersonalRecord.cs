using System.ComponentModel.DataAnnotations;

namespace Lift.Buddy.Core.Database.Entities
{
    //TODO: mettere ExerciseName, ExcerciseType in Excercise ed usare quello?
    public class PersonalRecord
    {
        [Key]
        public Guid Id { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public int Series { get; set; }
        public int Reps { get; set; }
        public Weight? Weight { get; set; }

        public virtual Exercise Exercise { get; set; }
    }

    public enum UnitOfMeasure
    {
        KG,
        LB
    }

    public record Weight(double Amount, UnitOfMeasure UnitOfMeasure);
}
