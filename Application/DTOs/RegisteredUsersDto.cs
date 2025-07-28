namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class RegisteredUsersDto
    {
        public Guid Id { get; set; }
        public string UserID { get; set; } 
        public string UserName { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
