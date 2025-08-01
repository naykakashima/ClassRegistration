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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public SubjectRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Subject>> GetAllAsync(CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Subjects
                .Include(s => s.Classes)
                .Include(s => s.Survey)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Subject?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Subjects
                .Include(s => s.Classes)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task AddAsync(Subject subject, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            await context.Subjects.AddAsync(subject, ct);
            await context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Subject subject, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Subjects.Update(subject);
            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Subject subject, CancellationToken ct = default)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Subjects.Remove(subject);
            await context.SaveChangesAsync(ct);
        }

        // Transaction-support versions (optional)
        public async Task AddAsync(Subject subject, AppDbContext context, CancellationToken ct = default)
        {
            await context.Subjects.AddAsync(subject, ct);
            // Let caller handle SaveChanges
        }

        public async Task UpdateAsync(Subject subject, AppDbContext context, CancellationToken ct = default)
        {
            context.Subjects.Update(subject);
            // Let caller handle SaveChanges
        }

        public async Task DeleteAsync(Subject subject, AppDbContext context, CancellationToken ct = default)
        {
            context.Subjects.Remove(subject);
            // Let caller handle SaveChanges
        }
    }
}