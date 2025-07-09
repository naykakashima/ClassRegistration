using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _db;

        public RegistrationRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Registration registration)
        {
            await _db.Registrations.AddAsync(registration);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid classId, Guid userId)
        {
            return await _db.Registrations.AnyAsync(r => r.ClassId == classId && r.UserId == userId);
        }

        public async Task<int> GetCountForClassAsync(Guid classId)
        {
            return await _db.Registrations
                .Where(r => r.ClassId == classId)
                .CountAsync();
        }
    }
}
