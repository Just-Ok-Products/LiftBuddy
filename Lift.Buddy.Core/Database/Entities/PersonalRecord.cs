using System.ComponentModel.DataAnnotations;

namespace Lift.Buddy.Core.Database.Entities
{
    //TODO: mettere ExerciseName, ExcerciseType in Excercise ed usare quello?
    public class PersonalRecord
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string ExerciseName { get; set; } = string.Empty;
        [Required]
        public int Series { get; set; }
        [Required]
        public int Reps { get; set; }

        public double? Weight { get; set; }
        public int? UOM { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}
