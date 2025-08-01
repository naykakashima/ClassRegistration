using System.ComponentModel.DataAnnotations;

namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class SurveyBase
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
    }
}
