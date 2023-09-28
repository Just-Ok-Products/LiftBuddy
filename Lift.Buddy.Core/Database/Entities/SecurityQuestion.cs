using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lift.Buddy.Core.Database.Entities;

public class SecurityQuestion
{
    [Key]
    public Guid Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }

    public virtual User User { get; set; }
}
