using ClassRegistrationApplication2025.Application.DTOs;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface ISubjectSurveyService
    {
        Task<List<SubjectSurveyDto>> GetAllAsync();

        Task<SubjectSurveyDto?> GetByIdAsync(Guid id);

        Task<SubjectSurveyDto?> GetBySubjectIdAsync(Guid subjectId);

        Task AddAsync(SubjectSurveyDto survey);

        Task UpdateAsync(SubjectSurveyDto survey);

        Task DeleteAsync(Guid id);
        Task CloseSurveyAsync(Guid surveyId);
        Task ReleaseSurveyAsync(Guid surveyId);
    }
}
