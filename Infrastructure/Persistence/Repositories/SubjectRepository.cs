using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _db;
        public SubjectRepository(AppDbContext db) => _db = db;

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _db.Subjects
                .Include(s => s.Classes)
                .ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _db.Subjects
                .Include(s => s.Classes)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Subject subject, AppDbContext context, CancellationToken ct = default)
        {
            // Add the new subject using the provided context (from UseCase)
            await context.Subjects.AddAsync(subject, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Subject subject, AppDbContext context, CancellationToken ct = default)
        {
            // Attach if not tracked
            context.Subjects.Update(subject);
            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Subject subject, AppDbContext context, CancellationToken ct = default)
        {
            context.Subjects.Remove(subject);
            await context.SaveChangesAsync(ct);
        }
    }
}

