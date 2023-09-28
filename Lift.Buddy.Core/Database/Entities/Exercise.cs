using System.ComponentModel.DataAnnotations;

namespace Lift.Buddy.Core.Database.Entities
{
    public class Exercise
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Repetitions { get; set; }
        public int? Series { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? Rest { get; set; }

        //TODO: spostare in DTO
        //public override string ToString() => $"{Name}: {Repetitions}x{Series}";
    }
}
