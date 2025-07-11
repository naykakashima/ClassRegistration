using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _db;

        public ClassRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Class newClass, AppDbContext context, CancellationToken ct)
        {
            context.Classes.Add(newClass);
            await context.SaveChangesAsync(ct);
        }
        public async Task<Class?> GetByIdAsync(Guid classId)
        {
            // Eager-load registrations if needed
            return await _db.Classes
                .Include(c => c.Registrations)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }

        public async Task<List<Class>> GetAllAsync()
        {
            // Include Registrations so we can count them if needed
            return await _db.Classes
                            .Include(c => c.Registrations)
                            .ToListAsync();
        }
    }
}
