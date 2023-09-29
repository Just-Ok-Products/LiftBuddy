namespace Lift.Buddy.Core.Database.Entities;

public class Exercise
{
    public Guid ExerciseId { get; set; }
    public string Name { get; set; }
    public int? Repetitions { get; set; }
    public int? Series { get; set; }
    public TimeOnly? Time { get; set; }
    public TimeOnly? Rest { get; set; }

    public virtual WorkoutDay WorkoutDay { get; set; }
}
