using ClassRegistrationApplication2025.Application.DTOs;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IEmailService
    {
        Task SendRegistrationConfirmationAsync(UserDto user, ClassDetailDto classInfo);
        Task SendSurveyInviteAsync(UserDto user);
    }
}
