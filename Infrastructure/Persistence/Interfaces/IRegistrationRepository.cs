using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<int> GetRegistrationCountByClassAsync(Guid classId);
        Task<bool> ExistsAsync(Guid classId, Guid userId);
        Task RegisterUserAsync(Guid userId, Guid classId, string Name,  CancellationToken ct);
        Task<List<Guid>> GetClassIdsByUserAsync(Guid userId);

    }
}
