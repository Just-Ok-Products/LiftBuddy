using System.ComponentModel.DataAnnotations;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.Core.Database.Entities
{
    //TODO: iirc esiste un'interfaccia/classe base per le entità di EF (mi pare sia proprio Entity).
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Gender Gender { get; set; }

        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public bool IsAdmin { get; set; } = false;
        [Required]
        public bool IsTrainer { get; set; } = false;

        public virtual ICollection<WorkoutPlan>? WorkoutPlans { get; set; }
        public virtual ICollection<PersonalRecord>? PersonalRecords { get; set; }
        public virtual ICollection<SecurityQuestion>? SecurityQuestions { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        NonBinary,
        NotSpecified
    }
}
