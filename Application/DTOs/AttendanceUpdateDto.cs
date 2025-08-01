namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class AttendanceUpdateDto
    {
        public Guid RegistrationId { get; set; }
        public DateTime? AttendedAt { get; set; }
    }
}
