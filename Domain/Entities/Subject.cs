using System.ComponentModel.DataAnnotations;

namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public List<Class> Classes { get; set; } = new();
    }
}
