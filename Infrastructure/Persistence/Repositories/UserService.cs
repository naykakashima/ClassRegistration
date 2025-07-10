using ClassRegistrationApplication2025.Application.DTOs;
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
        private readonly IHttpContextAccessor _http;
        private UserDto _cachedUser;

        public UserService(AppDbContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }

        public async Task<UserDto> GetOrCreateCurrentUserAsync(string adUserId)
        {
            if (_cachedUser != null) return _cachedUser;

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == adUserId);

            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    UserID = adUserId,
                    Name = adUserId,
                    Role = Role.User
                };

                try
                {
                    _db.Users.Add(user);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("duplicate") == true)
                {
                    // Duplicate user detected, likely due to race condition or refresh
                    user = await _db.Users.FirstAsync(u => u.UserID == adUserId);
                }
            }

            _cachedUser = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserID = user.UserID,
                Role = user.Role
            };

            return _cachedUser;
        }


        public async Task<bool> IsUserAuthorizedAsync(string adUserId)
        {
            var user = await GetOrCreateCurrentUserAsync(adUserId);
            return user != null;
        }

    }

}


