using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IClassRepository
    {
        // Standard methods
        Task AddAsync(Class newClass, CancellationToken ct);
        Task<Class?> GetByIdAsync(Guid classId, CancellationToken ct = default);
        Task<List<Class>> GetAllAsync(CancellationToken ct = default);
        Task<Class> GetClassWithRegistrationsAsync(Guid classId, CancellationToken ct = default);
        Task DeleteClassAsync(Guid classId, CancellationToken ct);
        Task<List<Class>> GetClassesBySubjectIdAsync(Guid subjectId, CancellationToken ct = default);
        Task<Guid?> GetSubjectIdByClassIdAsync(Guid classId, CancellationToken ct = default);
        Task<List<Class>> GetClassesByIdsAsync(IEnumerable<Guid> classIds, CancellationToken ct = default);

        // Transaction support methods (optional)
        Task AddAsync(Class newClass, AppDbContext context, CancellationToken ct);
        Task DeleteClassAsync(Guid classId, AppDbContext context, CancellationToken ct);
    }
}