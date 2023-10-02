namespace Lift.Buddy.Core.Database.Entities;

public class PersonalRecord
{
    public Guid PersonalRecordId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public int Series { get; set; }
    public int Reps { get; set; }

    public double? Weight { get; set; }
    public int? UOM { get; set; }

    public Guid ExerciseId { get; set; }
    public virtual Exercise Exercise { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
