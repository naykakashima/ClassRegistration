using ClassRegistrationApplication2025.Domain.Entities;
using System.Security.Claims;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IUserService
    {
        Task<User> GetOrCreateCurrentUserAsync(ClaimsPrincipal principal);
    }
}
