using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface ISurveyRepository
    {
        Task AddAsync(Survey newSurvey, AppDbContext context, CancellationToken ct);

        Task<Survey?> GetByIdAsync(Guid surveyId);

        Task<List<Survey>> GetAllAsync();

        Task<Survey?> GetBySubjectIdAsync(Guid subjectId);

        Task UpdateAsync(Survey survey, AppDbContext context, CancellationToken ct);

        Task DeleteAsync(Guid surveyId, AppDbContext context, CancellationToken ct);
    }
}
