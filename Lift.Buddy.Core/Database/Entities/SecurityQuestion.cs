namespace Lift.Buddy.Core.Database.Entities;

public class SecurityQuestion
{
    public Guid SecurityQuestionId { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }

    public virtual User User { get; set; }
}
