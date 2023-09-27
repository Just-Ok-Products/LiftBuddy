using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    //TODO: iirc esiste un'interfaccia/classe base per le entità di EF (mi pare sia proprio Entity).
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsTrainer { get; set; } = false;

        //QUESTION: domande e risposte in chiaro su db?
        public string Questions { get; set; }
        public string Answers { get; set; }

        [JsonIgnore]
        public virtual ICollection<WorkoutAssignment>? WorkoutAssignments { get; set; }
        [JsonIgnore]
        public virtual ICollection<WorkoutPlan>? WorkoutSchedules { get; set; }
        [JsonIgnore]
        public virtual UserPersonalRecord UserPersonalRecord { get; set; }
    }
}
