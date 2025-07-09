using ClassRegistrationApplication2025.Domain.Entities;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IClassRepository
    {
        Task AddAsync(Class newClass, CancellationToken cancellationToken = default);
    }
}
