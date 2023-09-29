namespace Lift.Buddy.Core.Models;

public class ExerciseDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public int? Repetitions { get; set; }
    public int? Series { get; set; }
    public TimeOnly? Time { get; set; }
    public TimeOnly? Rest { get; set; }

    public override string ToString() => $"{Name}: {Repetitions}x{Series}";
}
