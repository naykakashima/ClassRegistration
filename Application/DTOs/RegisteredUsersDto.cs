using System.Text.Json.Serialization;

namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class RegisteredUsersDto
    {
        public Guid Id { get; set; }                 
        public string UserID { get; set; }            
        public string UserName { get; set; }          
        public string EmailSMTP { get; set; }         
        public DateTime RegisteredAt { get; set; }   
        public DateTime? AttendedAt { get; set; }


        [JsonIgnore]
        public Guid RegistrationId => Id;

        [JsonIgnore]
        public bool IsAttended
        {
            get => AttendedAt != null;
            set => AttendedAt = value ? DateTime.UtcNow : null;
        }
    }
}
