using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using System.Security.Claims;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetOrCreateCurrentUserAsync(string adUserId);
        Task<bool> IsUserAuthorizedAsync(string adUserId);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> UpdateUserRoleAsync(Guid userId, Role newRole, string actingUserAdId);
    }
}
