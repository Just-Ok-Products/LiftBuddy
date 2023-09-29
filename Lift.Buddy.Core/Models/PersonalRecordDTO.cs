namespace Lift.Buddy.Core.Models;

public class PersonalRecordDTO
{
    public Guid? Id { get; set; }
    public int Series { get; set; }
    public int Reps { get; set; }
    public Weight? Weight { get; set; }

    public virtual ExerciseDTO Exercise { get; set; }
}

public enum UnitOfMeasure
{
    Undefined,
    KG,
    LB
}

public record Weight(double Amount, UnitOfMeasure UnitOfMeasure);
