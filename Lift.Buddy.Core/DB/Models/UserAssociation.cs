using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Lift.Buddy.Core.DB.Models
{
    public class UserAssociation
    {
        [Key]
        public string TrainerUsername { get; set; } = "";
        [Key]
        public string AthleteUsername { get; set; } = "";

        [ForeignKey(nameof(TrainerUsername)), IgnoreDataMember]
        public virtual User Trainer { get; set; }

        [ForeignKey(nameof(AthleteUsername)), IgnoreDataMember]
        public virtual User Athlete { get; set; }

    }
}
