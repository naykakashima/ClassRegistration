using ClassRegistrationApplication2025.Application.DTOs;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface ISurveyService
    {
        Task<List<SurveyDto>> GetAllAsync();

        Task<SurveyDto?> GetByIdAsync(Guid id);

        Task<SurveyDto?> GetBySubjectIdAsync(Guid subjectId);

        Task AddAsync(SurveyDto survey);

        Task UpdateAsync(SurveyDto survey);

        Task DeleteAsync(Guid id);
    }
}
