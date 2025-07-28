namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class RegisteredUsersDto
    {
        public Guid Id { get; set; }           // Registration Id (optional, but useful)
        public string UserName { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
