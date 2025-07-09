using ClassRegistrationApplication2025.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                // Recommend index on the AD identifier
                entity.HasIndex(u => u.UserID).IsUnique();
            });

            // Registration relationships
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasOne(r => r.Class)
                      .WithMany(c => c.Registrations)
                      .HasForeignKey(r => r.ClassId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Registrations)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }


}
