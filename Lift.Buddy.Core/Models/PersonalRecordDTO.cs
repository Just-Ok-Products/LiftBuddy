using System.Text.Json.Serialization;
using Lift.Buddy.Core.Models.Enums;

namespace Lift.Buddy.Core.Models;

// TODO : in futuro sostituire ExerciseName/Type con Exercise, modificare richiesta da frontend per inviare e ricevere Exercise
public class PersonalRecordDTO
{
    public Guid? Id { get; set; }
    public string ExerciseName { get; set; }
    public int Series { get; set; }
    public int Reps { get; set; }
    public double? Weight { get; set; }
    public Guid UserId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UnitOfMeasure UnitOfMeasure { get; set; }
    public ExerciseType ExerciseType { get; set; }
}

public enum UnitOfMeasure
{
    Undefined,
    KG,
    LB
}

public class PersonalRecords
{
    public IEnumerable<PersonalRecordDTO> ToUpdate { get; set; }
    public IEnumerable<PersonalRecordDTO> ToAdd { get; set; }
}

//public record Weight(double Amount, UnitOfMeasure UnitOfMeasure);