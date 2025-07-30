using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(Guid id);
        Task AddAsync(Subject subject, AppDbContext context, CancellationToken ct = default);
        Task UpdateAsync(Subject subject, AppDbContext context, CancellationToken ct = default);
    }
}
