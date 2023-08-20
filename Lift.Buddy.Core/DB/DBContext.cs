using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lift.Buddy.Core.DB
{
    public partial class DBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<WorkoutSchedule> WorkoutSchedules { get; set; }

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("TestDatabase"));
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

            OnModelCreatingPartial(modelBuilder);
        }
        #region Methods
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

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
