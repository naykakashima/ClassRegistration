using ClassRegistrationApplication2025.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
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

            // Class CreatedByUser + Subject relationships
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasOne(c => c.CreatedByUser)
                      .WithMany(u => u.ClassesCreated)
                      .HasForeignKey(c => c.CreatedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Subject)
                      .WithMany(s => s.Classes)
                      .HasForeignKey(c => c.SubjectId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Survey)
                      .WithOne(s => s.Class)
                      .HasForeignKey<Class>(c => c.SurveyId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasOne(s => s.Survey)
                      .WithOne(sv => sv.Subject)
                      .HasForeignKey<Subject>(s => s.SurveyId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasOne(s => s.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(s => s.CreatedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SurveyResponse>(entity =>
            {
                entity.HasOne(sr => sr.Survey)
                      .WithMany()
                      .HasForeignKey(sr => sr.SurveyId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sr => sr.User)
                      .WithMany()
                      .HasForeignKey(sr => sr.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(sr => new { sr.SurveyId, sr.UserId }).IsUnique(); // Enforce one response per user per survey
            });
        }
    }
}
