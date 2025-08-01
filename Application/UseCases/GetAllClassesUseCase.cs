using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetAllClassesUseCase
    {
        private readonly IClassRepository _classRepo;

        public GetAllClassesUseCase(IClassRepository classRepo)
        {
            _classRepo = classRepo;
        }

        public async Task<List<ClassSummaryDto>> ExecuteAsync(CancellationToken ct = default)
        {
            var classes = await _classRepo.GetAllAsync(ct);

            // Map entities to DTOs, including counting registrations
            return classes.Select(c => new ClassSummaryDto
            {
                Id = c.Id,
                ClassName = c.ClassName,
                SessionName = c.SessionName,
                Presenter = c.Presenter,
                Date = c.Date,
                StartTime = c.StartTime,
                EndTime = c.EndTime,
                MaxSlots = c.MaxSlots,
                RegisteredCount = c.Registrations?.Count ?? 0, // Null-safe count
                Status = c.Status
            }).ToList();
        }
    }
}