namespace Lift.Buddy.Core.Database.Entities;

public class WorkoutPlan
{
    public WorkoutPlan()
    {
        Users = new HashSet<User>();
        WorkoutDays = new HashSet<WorkoutDay>();
    }

    public Guid WorkoutPlanId { get; set; }

    public string Name { get; set; }
    public float ReviewAverage { get; set; }
    public int ReviewCount { get; set; }
    public Guid CreatorId { get; set; }

    public virtual User Creator { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }
}
