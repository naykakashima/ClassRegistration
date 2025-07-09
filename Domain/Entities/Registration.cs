namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class Registration
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public required Class Class { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
