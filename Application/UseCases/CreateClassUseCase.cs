using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class CreateClassUseCase
    {
        private readonly IClassRepository _classRepo;

        public CreateClassUseCase(IClassRepository classRepo)
        {
            _classRepo = classRepo;
        }

        public async Task ExecuteAsync(CreateClassDto dto, CancellationToken ct = default)
        {
            if (dto.Date == null || dto.StartTime == null || dto.EndTime == null)
                throw new ArgumentException("Date and time fields are required.");

            var newClass = new Class
            {
                Id = Guid.NewGuid(),
                ClassName = dto.ClassName,
                Presenter = dto.PresenterName,
                Date = dto.Date.Value,
                StartTime = dto.StartTime.Value,
                EndTime = dto.EndTime.Value,
                MaxSlots = dto.MaxSlots,
                Status = ClassStatus.Open
            };

            await _classRepo.AddAsync(newClass, ct);
        }

    }
}
