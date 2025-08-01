using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<int> GetRegistrationCountByClassAsync(Guid classId);
        Task<bool> ExistsAsync(Guid classId, Guid userId);
        Task RegisterUserAsync(Guid userId, Guid classId, string Name, string EmailSMTP, CancellationToken ct);
        Task UnregisterUserAsync(Guid userId, Guid classId, CancellationToken ct);
        Task<List<Guid>> GetClassIdsByUserAsync(Guid userId);
        Task<List<Registration>> GetRegistrationsByClassAsync(Guid classId);
        Task UpdateAttendanceAsync(List<AttendanceUpdateDto> updates, CancellationToken ct);
        Task<List<Registration>> GetRegistrationsByUserIdAsync(Guid userId, CancellationToken ct);
    }
}
