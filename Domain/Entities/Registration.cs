#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class Registration
    {
        public Guid Id { get; set; }

        // Class relationship
        [ForeignKey("Class")]
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        // User relationship (GUID-based)
        [ForeignKey("User")]
        public Guid UserId { get; set; } // References User.Id (GUID)
        public User User { get; set; }

        // Denormalized for display/audit
        public string UserName { get; set; }
        public string EmailSMTP { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
