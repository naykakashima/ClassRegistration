using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Helpers;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IHttpContextAccessor _http;
        private readonly IOptions<AdSettings> _adSettings;
        private UserDto _cachedUser;

        public UserService(IDbContextFactory<AppDbContext> contextFactory, IHttpContextAccessor http, IOptions<AdSettings> adSettings)
        {
            _contextFactory = contextFactory;
            _http = http;
            _adSettings = adSettings;
        }

        public async Task<UserDto> GetOrCreateCurrentUserAsync(string adUserId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            if (_cachedUser != null) return _cachedUser;

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserID == adUserId);
            var displayName = AdHelper.GetDisplayNameFromAd(adUserId, _adSettings.Value.LdapPath) ?? adUserId;
            var properties = AdHelper.DumpUserProperties(adUserId, _adSettings.Value.LdapPath);
            var email = AdHelper.ExtractPrimarySmtpFromPropertiesDump(properties) ?? adUserId;



            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    UserID = adUserId,
                    Name = displayName,
                    Role = Role.User,
                    EmailSMTP = email
                };

                try
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("duplicate") == true)
                {
                    // Duplicate user detected, likely due to race condition or refresh
                    user = await context.Users.FirstAsync(u => u.UserID == adUserId);
                }
            }

            _cachedUser = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserID = user.UserID,
                Role = user.Role,
                EmailSMTP = user.EmailSMTP
            };

            return _cachedUser;
        }


        public async Task<bool> IsUserAuthorizedAsync(string adUserId)
        {
            using var context = _contextFactory.CreateDbContext();

            var user = await GetOrCreateCurrentUserAsync(adUserId);
            return user != null;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Users
                .OrderBy(u => u.Role)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    UserID = u.UserID,
                    Role = u.Role,
                    EmailSMTP = u.EmailSMTP
                })
                .ToListAsync();

        }

        public async Task<bool> UpdateUserRoleAsync(Guid userId, Role newRole, string actingUserAdId)
        {
            using var context = _contextFactory.CreateDbContext();

            var actingUser = await context.Users.FirstOrDefaultAsync(u => u.UserID == actingUserAdId);
            if (actingUser == null || actingUser.Role != Role.SuperAdmin)
                throw new UnauthorizedAccessException("Only SuperAdmins can assign roles.");

            var user = await context.Users.FindAsync(userId);
            if (user == null) return false;

            user.Role = newRole;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByAdUserIdAsync(string adUserId, CancellationToken ct = default)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Users.FirstOrDefaultAsync(u => u.UserID == adUserId, ct);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserID = user.UserID,
                Role = user.Role,
                EmailSMTP = user.EmailSMTP
            };
        }



    }
}


