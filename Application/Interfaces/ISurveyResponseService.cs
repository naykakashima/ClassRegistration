using ClassRegistrationApplication2025.Application.DTOs;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface ISurveyResponseService
    {
        Task<SurveyResponseDto?> GetResponseAsync(Guid surveyId, Guid userId);
        Task<List<SurveyResponseDto>> GetResponsesBySurveyIdAsync(Guid surveyId);
        Task AddResponseAsync(SurveyResponseDto responseDto);
    }
}
