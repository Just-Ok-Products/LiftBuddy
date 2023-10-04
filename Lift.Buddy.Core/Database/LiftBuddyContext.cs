using Lift.Buddy.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.Core.Database;

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
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.UserId).ValueGeneratedNever();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(32);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.IsTrainer).IsRequired().HasDefaultValue(false);
            entity.Property(u => u.IsAdmin).IsRequired().HasDefaultValue(false);

            entity.HasMany(u => u.SecurityQuestions)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.PersonalRecords)
                .WithOne(pr => pr.User)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.AssignedPlans)
                .WithMany(p => p.Users);
        });

        modelBuilder.Entity<WorkoutPlan>(entity =>
        {
            entity.HasKey(p => p.WorkoutPlanId);
            entity.Property(p => p.WorkoutPlanId).ValueGeneratedNever();

            entity.HasOne(p => p.Creator)
                .WithMany(p => p.CreatedPlans)
                .HasForeignKey(p => p.CreatorId);

            entity.HasMany(p => p.WorkoutDays)
                .WithOne(p => p.WorkoutPlan)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkoutDay>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.HasMany(d => d.Exercises)
                .WithOne(e => e.WorkoutDay)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.WorkoutPlan)
                .WithMany(p => p.WorkoutDays);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId);

        });

        modelBuilder.Entity<PersonalRecord>(entity =>
        {
            entity.HasKey(r => r.PersonalRecordId);
        });

        modelBuilder.Entity<SecurityQuestion>(entity =>
        {
            entity.HasKey(sq => sq.SecurityQuestionId);
        });

        base.OnModelCreating(modelBuilder);
    }
}

