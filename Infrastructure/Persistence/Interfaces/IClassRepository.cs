using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IClassRepository
    {
        Task AddAsync(Class newClass, AppDbContext context, CancellationToken ct);
        Task<Class?> GetByIdAsync(Guid classId);
    }
}
