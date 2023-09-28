namespace Lift.Buddy.Core.Models
{
    public class WorkoutDayDTO
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }

        public virtual ICollection<ExerciseDTO> Exercises { get; set; }
    }
}
