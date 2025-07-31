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
        public DbSet<SurveyBase> Surveys { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.UserID).IsUnique();

                // Define property requirements explicitly if needed
                entity.Property(u => u.UserID).IsRequired();
                entity.Property(u => u.Name).IsRequired();
                entity.Property(u => u.EmailSMTP).IsRequired();
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

                entity.Property(r => r.UserName).IsRequired();
                entity.Property(r => r.EmailSMTP).IsRequired();
            });

            // Class entity
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
                      .HasForeignKey<ClassSurvey>(cs => cs.ClassId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.Property(c => c.ClassName).IsRequired();
                entity.Property(c => c.Presenter).IsRequired();
            });

            // Subject entity
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasOne(s => s.Survey)
                      .WithOne(sv => sv.Subject)
                      .HasForeignKey<SubjectSurvey>(sv => sv.SubjectId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.Property(s => s.Title).IsRequired();
            });

            // SurveyBase and discriminator
            modelBuilder.Entity<SurveyBase>(entity =>
            {
                entity.HasDiscriminator<string>("SurveyType")
                      .HasValue<SubjectSurvey>("SubjectSurvey")
                      .HasValue<ClassSurvey>("ClassSurvey");

                entity.HasOne(s => s.CreatedByUser)
                      .WithMany()
                      .HasForeignKey(s => s.CreatedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(s => s.Title).IsRequired();
                entity.Property(s => s.JsonDefinition).IsRequired();
            });

            // SurveyResponse entity
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

                entity.HasIndex(sr => new { sr.SurveyId, sr.UserId }).IsUnique();

                entity.Property(sr => sr.JsonAnswers).IsRequired();
            });
        }
    }
}
