using System.ComponentModel.DataAnnotations;

namespace Lift.Buddy.Core.Database.Entities
{
    public class WorkoutPlan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public float ReviewAverage { get; set; }
        public int ReviewCount { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }
    }
}
