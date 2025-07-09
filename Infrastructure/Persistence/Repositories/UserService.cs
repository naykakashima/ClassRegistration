using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetOrCreateCurrentUserAsync(ClaimsPrincipal principal)
        {
            var adUserId = principal.Identity?.Name; // e.g., "DOMAIN\\user1"

            if (string.IsNullOrEmpty(adUserId))
                throw new UnauthorizedAccessException("Not logged in.");

            // Search by AD UserID - NOT Guid Id
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.UserID == adUserId);

            if (existingUser != null)
                return existingUser;

            // Auto-create new user with fresh GUID for Id
            var newUser = new User
            {
                Id = Guid.NewGuid(),       // Generate new GUID
                UserID = adUserId,         // Store AD identifier
                Name = principal.FindFirst(ClaimTypes.Name)?.Value ?? adUserId,
                Role = DetermineInitialRole(principal)
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return newUser;
        }

        private Role DetermineInitialRole(ClaimsPrincipal principal)
        {
            // Default: All new users are 'User' role
            return Role.User;
        }
    }
}
    

