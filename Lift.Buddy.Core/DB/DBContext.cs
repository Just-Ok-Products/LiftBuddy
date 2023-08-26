using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lift.Buddy.Core.DB
{
    public class DBContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<WorkoutSchedule> WorkoutSchedules { get; set; }
        public DbSet<WorkoutAssignment> WorkoutAssignments { get; set; }
        public DbSet<UserPR> UserPRs { get; set; }

        public DBContext(DbContextOptions<DBContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Surname);

                entity.Property(e => e.IsAdmin).IsRequired()
                    .HasDefaultValue(false);

                entity.Property(e => e.IsAdmin).IsRequired();

                entity.Property(e => e.IsAdmin).IsRequired();
            });

            modelBuilder.Entity<WorkoutSchedule>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.WorkoutDays)
                    .HasConversion<string>(exercises => TrainingToString(exercises), dbExercises => StringToTraining(dbExercises));
            });

            modelBuilder.Entity<WorkoutAssignment>(entity =>
            {
                entity.HasKey(e => e.WorkoutId);
                entity.HasKey(e => e.WorkoutUser);

            });

            modelBuilder.Entity<WorkoutSchedule>()
                .HasMany(e => e.WorkoutAssignments)
                .WithOne(e => e.WorkoutSchedule)
                .HasForeignKey(e => e.WorkoutId)
                .HasPrincipalKey(e => e.Id);


            modelBuilder.Entity<User>()
                .HasMany(e => e.WorkoutAssignments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.WorkoutUser)
                .HasPrincipalKey(e => e.UserName);

            modelBuilder.Entity<UserPR>(entity =>
            {
                entity.HasKey(e => e.Username);
                entity.Property(e => e.PersonalRecords)
                    .HasConversion<string>(exercises => PRToString(exercises), dbExercises => StringToPR(dbExercises));

                entity.HasOne(e => e.User)
                .WithOne(e => e.UserPR)
                .HasForeignKey<UserPR>(e => e.Username)
                .HasPrincipalKey<User>(e => e.UserName);
            });
        }

        #region Methods

        #region Training conversion
        private string TrainingToString(List<WorkoutDay> exercises)
        {
            return JsonSerializer.Serialize(exercises.ToArray());
        }
        private List<WorkoutDay> StringToTraining(string exercises)
        {
            var trainings = JsonSerializer.Deserialize<WorkoutDay[]>(exercises);
            if (trainings == null)
            {
                return new List<WorkoutDay>();
            }
            return trainings.ToList();
        }
        #endregion

        #region PR conversion
        private string PRToString(List<PersonalRecord> records)
        {
            return JsonSerializer.Serialize(records.ToArray());
        }
        private List<PersonalRecord> StringToPR(string exercises)
        {
            var trainings = JsonSerializer.Deserialize<PersonalRecord[]>(exercises);
            if (trainings == null)
            {
                return new List<PersonalRecord>();
            }
            return trainings.ToList();
        }
        #endregion

        #endregion
    }
}
