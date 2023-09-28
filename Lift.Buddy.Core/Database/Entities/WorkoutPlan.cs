using Lift.Buddy.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lift.Buddy.Core.Database.Entities
{
    public class WorkoutPlan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int ReviewsStars { get; set; } = 0;
        public int ReviewAverage { get; set; } = 0;

        public virtual User? Creator { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }
    }
}
