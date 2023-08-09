using Lift.Buddy.Core.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.Core.DB
{
    public partial class DBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
