using MigraDoc.DocumentObjectModel;
using System.Text.Json.Serialization;
using System;

namespace Lift.Buddy.Core.Database.Entities
{
    public class WorkoutDay
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
