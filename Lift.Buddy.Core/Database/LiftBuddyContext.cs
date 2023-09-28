using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lift.Buddy.Core.Database
{
    public class LiftBuddyContext : DbContext
    {

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<PersonalRecord> PersonalRecords { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }

        public LiftBuddyContext(DbContextOptions<LiftBuddyContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
