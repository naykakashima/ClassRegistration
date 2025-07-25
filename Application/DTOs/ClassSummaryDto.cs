using ClassRegistrationApplication2025.Domain.Enums;

namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class ClassSummaryDto
    {
        public Guid Id { get; set; }
        public string ClassName { get; set; }
        public string SessionName { get; set; }
        public string Presenter { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxSlots { get; set; }
        public int RegisteredCount { get; set; }
        public ClassStatus Status { get; set; }
    }
}
