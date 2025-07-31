using ClassRegistrationApplication2025.Application.DTOs;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface IClassSurveyService
    {
        Task<List<ClassSurveyDto>> GetAllAsync();

        Task<ClassSurveyDto?> GetByIdAsync(Guid id);

        Task<ClassSurveyDto?> GetByClassIdAsync(Guid subjectId);

        Task AddAsync(ClassSurveyDto survey);

        Task UpdateAsync(ClassSurveyDto survey);

        Task DeleteAsync(Guid id);
    }
}
