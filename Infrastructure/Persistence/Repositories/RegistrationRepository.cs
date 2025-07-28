using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _db;

        public RegistrationRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> GetRegistrationCountByClassAsync(Guid classId)
        {
            return await _db.Registrations.CountAsync(r => r.ClassId == classId);
        }

        public async Task<bool> ExistsAsync(Guid classId, Guid userId)
        {
            return await _db.Registrations.AnyAsync(r => r.ClassId == classId && r.UserId == userId);
        }

        public async Task RegisterUserAsync(Guid userId, Guid classId, string Name, CancellationToken ct)
        {
            var registration = new Registration
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ClassId = classId,
                UserName = Name,
                RegisteredAt = DateTime.UtcNow
            };

            _db.Registrations.Add(registration);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UnregisterUserAsync(Guid userId, Guid classId, CancellationToken ct)
        {
            var registration = await _db.Registrations
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ClassId == classId, ct);

            if (registration == null)
                return; // Or throw an exception depending on your business logic

            _db.Registrations.Remove(registration);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<List<Guid>> GetClassIdsByUserAsync(Guid userId)
        {
            return await _db.Registrations
                .Where(r => r.UserId == userId)
                .Select(r => r.ClassId)
                .ToListAsync();
        }
    }
}
