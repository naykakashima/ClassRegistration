﻿using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public async Task<Class> GetClassWithRegistrationsAsync(Guid classId, CancellationToken ct = default)
        {
            return await _db.Classes
                .Include(c => c.Registrations)
                .FirstOrDefaultAsync(c => c.Id == classId, ct);
        }

        public async Task DeleteClassAsync(Guid classId, AppDbContext context, CancellationToken ct)
        {
            var classToDelete = await context.Classes.FindAsync(classId);
            if (classToDelete != null)
            {
                context.Classes.Remove(classToDelete);
                await context.SaveChangesAsync(ct);
            }
        }
        public async Task<List<Class>> GetClassesBySubjectIdAsync(Guid subjectId)
        {
            return await _db.Classes
                .Where(c => c.SubjectId == subjectId)
                .Include(c => c.Registrations) // Optional: preload
                .Include(c => c.Survey) 
                .ToListAsync();
        }

        public async Task<Guid?> GetSubjectIdByClassIdAsync(Guid classId)
        {
            return await _db.Classes
                .Where(c => c.Id == classId)
                .Select(c => (Guid?)c.SubjectId)
                .FirstOrDefaultAsync();
        }


    }
}
