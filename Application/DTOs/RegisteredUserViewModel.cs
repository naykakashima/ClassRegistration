namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class RegisteredUserViewModel
    {
        public RegisteredUsersDto Dto { get; set; } = default!;
        public bool UseCustomUrl { get; set; }
        public string? CustomSurveyUrl { get; set; }
    }

}
