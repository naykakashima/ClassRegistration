using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class UpdateAttendanceUseCase
    {
        private readonly IRegistrationRepository _repo;

        public UpdateAttendanceUseCase(IRegistrationRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(List<AttendanceUpdateDto> updates, CancellationToken ct)
        {
            await _repo.UpdateAttendanceAsync(updates, ct);
        }
    }
}
