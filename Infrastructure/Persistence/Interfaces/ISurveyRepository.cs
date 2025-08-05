using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using DocumentFormat.OpenXml.Office2013.Excel;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces
{
    public interface ISurveyRepository
    {
        Task<SurveyBase?> GetByIdAsync(Guid surveyId);

        Task<List<SurveyBase>> GetAllAsync();
        Task AddAsync(SurveyBase newSurvey, CancellationToken ct);
        Task UpdateAsync(SurveyBase survey, CancellationToken ct);
        Task DeleteAsync(Guid surveyId, CancellationToken ct);

        Task<List<SubjectSurvey>> GetAllSubjectSurveysAsync();
        Task<SubjectSurvey?> GetBySubjectIdAsync(Guid subjectId);

        Task<List<ClassSurvey>> GetAllClassSurveysAsync();
        Task<ClassSurvey?> GetByClassIdAsync(Guid classId);
        Task<List<SurveyBase>> GetByCreatorUserIdAsync(Guid userId);
        Task<List<SubjectSurvey>> GetReleasedSubjectSurveysAsync();
    }
}
