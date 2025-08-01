using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public RegistrationRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int> GetRegistrationCountByClassAsync(Guid classId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Registrations.CountAsync(r => r.ClassId == classId);
        }

        public async Task<bool> ExistsAsync(Guid classId, Guid userId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Registrations.AnyAsync(r => r.ClassId == classId && r.UserId == userId);
        }

        public async Task RegisterUserAsync(Guid userId, Guid classId, string Name, string EmailSMTP, CancellationToken ct)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var registration = new Registration
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ClassId = classId,
                UserName = Name,
                EmailSMTP = EmailSMTP,
                RegisteredAt = DateTime.UtcNow
            };

            context.Registrations.Add(registration);
            await context.SaveChangesAsync(ct);
        }

        public async Task UnregisterUserAsync(Guid userId, Guid classId, CancellationToken ct)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var registration = await context.Registrations
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ClassId == classId, ct);

            if (registration == null) return;

            context.Registrations.Remove(registration);
            await context.SaveChangesAsync(ct);
        }

        public async Task<List<Guid>> GetClassIdsByUserAsync(Guid userId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Registrations
                .Where(r => r.UserId == userId)
                .Select(r => r.ClassId)
                .ToListAsync();
        }

        public async Task<List<Registration>> GetRegistrationsByClassAsync(Guid classId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Registrations
                .Include(r => r.User)
                .Where(r => r.ClassId == classId)
                .ToListAsync();
        }

        public async Task UpdateAttendanceAsync(List<AttendanceUpdateDto> updates, CancellationToken ct)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            foreach (var update in updates)
            {
                await context.Registrations
                    .Where(r => r.Id == update.RegistrationId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(r => r.AttendedAt, update.AttendedAt),
                    ct);
            }
        }

        public async Task<List<Registration>> GetRegistrationsByUserIdAsync(Guid userId, CancellationToken ct)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Registrations
                .Include(r => r.Class)
                .Where(r => r.UserId == userId)
                .ToListAsync(ct);
        }
    }
}