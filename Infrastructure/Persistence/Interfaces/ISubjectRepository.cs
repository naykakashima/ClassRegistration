using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync(CancellationToken ct = default);
        Task<Subject?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Subject subject, CancellationToken ct = default);
        Task UpdateAsync(Subject subject, CancellationToken ct = default);
        Task DeleteAsync(Subject subject, CancellationToken ct = default);
    }
}
