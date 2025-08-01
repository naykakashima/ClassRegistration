using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public ClassRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // Standard implementation
        public async Task AddAsync(Class newClass, CancellationToken ct)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            await AddAsync(newClass, context, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task<Class?> GetByIdAsync(Guid classId, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            return await context.Classes
                .Include(c => c.Registrations)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == classId, ct);
        }

        public async Task<List<Class>> GetAllAsync(CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            return await context.Classes
                .Include(c => c.Registrations)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Class> GetClassWithRegistrationsAsync(Guid classId, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            return await context.Classes
                .Include(c => c.Registrations)
                .FirstOrDefaultAsync(c => c.Id == classId, ct);
        }

        public async Task DeleteClassAsync(Guid classId, CancellationToken ct)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            await DeleteClassAsync(classId, context, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task<List<Class>> GetClassesBySubjectIdAsync(Guid subjectId, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            return await context.Classes
                .Where(c => c.SubjectId == subjectId)
                .Include(c => c.Registrations)
                .Include(c => c.Survey)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Guid?> GetSubjectIdByClassIdAsync(Guid classId, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            var entity = await context.Classes
                .Where(c => c.Id == classId)
                .AsNoTracking()
                .FirstOrDefaultAsync(ct);

            return entity?.SubjectId;
        }

        public async Task<List<Class>> GetClassesByIdsAsync(IEnumerable<Guid> classIds, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync(ct);
            return await context.Classes
                .Where(c => classIds.Contains(c.Id))
                .Include(c => c.Subject)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        // Transaction support implementation
        public async Task AddAsync(Class newClass, AppDbContext context, CancellationToken ct)
        {
            await context.Classes.AddAsync(newClass, ct);
            // Let caller handle SaveChanges
        }

        public async Task DeleteClassAsync(Guid classId, AppDbContext context, CancellationToken ct)
        {
            var classToDelete = await context.Classes.FindAsync(classId);
            if (classToDelete != null)
            {
                context.Classes.Remove(classToDelete);
                // Let caller handle SaveChanges
            }
        }
    }
}