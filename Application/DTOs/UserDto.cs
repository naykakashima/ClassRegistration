using ClassRegistrationApplication2025.Domain.Enums;

namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
