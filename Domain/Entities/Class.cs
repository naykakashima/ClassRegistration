using ClassRegistrationApplication2025.Domain.Enums;

namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class Class
    {
        public Guid Id { get; set; }
        public required string ClassName { get; set; }
        public string SessionName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxSlots { get; set; }
        public required string Presenter { get; set; }
        public ClassStatus Status { get; set; }
        public List<Registration> Registrations { get; set; } = new();
        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }

}

