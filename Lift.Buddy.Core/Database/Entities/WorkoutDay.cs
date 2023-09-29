namespace Lift.Buddy.Core.Database.Entities;

public class WorkoutDay
{
    public WorkoutDay()
    {
        Exercises = new HashSet<Exercise>();
    }

    public Guid Id { get; set; }
    public DayOfWeek Day { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; }
    public virtual WorkoutPlan WorkoutPlan { get; set; }
}
