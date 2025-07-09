using ClassRegistrationApplication2025.Domain.Entities;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IRegistrationRepository
    {
        Task AddAsync(Registration registration);
        Task<bool> ExistsAsync(Guid classId, Guid userId);
        Task<int> GetCountForClassAsync(Guid classId);
    }
}
