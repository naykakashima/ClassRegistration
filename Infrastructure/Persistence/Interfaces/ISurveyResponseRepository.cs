using ClassRegistrationApplication2025.Domain.Entities;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface ISurveyResponseRepository
    {
        Task<SurveyResponse?> GetBySurveyAndUserAsync(Guid surveyId, Guid userId);
        Task<List<SurveyResponse>> GetBySurveyIdAsync(Guid surveyId);
        Task AddAsync(SurveyResponse response);
    }
}
