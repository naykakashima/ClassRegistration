using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IClassRepository
    {
        Task AddAsync(Class newClass, AppDbContext context, CancellationToken ct);
        Task<Class?> GetByIdAsync(Guid classId);
        Task<List<Class>> GetAllAsync();
        Task<Class> GetClassWithRegistrationsAsync(Guid classId, CancellationToken ct = default);
        Task DeleteClassAsync(Guid classId, AppDbContext context, CancellationToken ct);
        Task<List<Class>> GetClassesBySubjectIdAsync(Guid subjectId);
        Task<Guid?> GetSubjectIdByClassIdAsync(Guid classId);
    }
}
