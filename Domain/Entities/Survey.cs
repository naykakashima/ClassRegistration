using System.ComponentModel.DataAnnotations;

namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class Survey
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string JsonDefinition { get; set; }

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional reverse nav props
        public Class? Class { get; set; }
        public Guid? SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
