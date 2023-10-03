namespace Lift.Buddy.Core.Database.Entities;

public class User
{
    public User()
    {
        AssignedPlans = new HashSet<WorkoutPlan>();
        CreatedPlans = new HashSet<WorkoutPlan>();
        PersonalRecords = new HashSet<PersonalRecord>();
        SecurityQuestions = new HashSet<SecurityQuestion>();
    }

    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; } = false;
    public bool IsTrainer { get; set; } = false;

    public virtual ICollection<WorkoutPlan> AssignedPlans { get; set; }
    public virtual ICollection<WorkoutPlan> CreatedPlans { get; set; }
    public virtual ICollection<PersonalRecord> PersonalRecords { get; set; }
    public virtual ICollection<SecurityQuestion> SecurityQuestions { get; set; }
}

public enum Gender
{
    Male,
    Female,
    NonBinary,
    NotSpecified
}
