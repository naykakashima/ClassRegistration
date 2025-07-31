using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetAllClassesBySubjectIdUseCase
    {
        private readonly IClassRepository _classRepo;

        public GetAllClassesBySubjectIdUseCase(IClassRepository classRepo)
        {
            _classRepo = classRepo;
        }

        public async Task<List<ClassSummaryDto>> ExecuteAsync(Guid subjectId)
        {
            var classes = await _classRepo.GetClassesBySubjectIdAsync(subjectId);

            // You map your domain entities to DTOs here.
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
                RegisteredCount = c.Registrations.Count, // Or a query count
                Status = c.Status,
                HasSurvey = c.Survey != null
            }).ToList();
        }
    }
}
    