using ClassRegistrationApplication2025.Application.DTOs;
using SurveyBuilder.Models;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface ISurveyResponseService
    {
        Task<SurveyResponseDto?> GetResponseAsync(Guid surveyId, Guid userId);
        Task<List<SurveyResponseDto>> GetResponsesBySurveyIdAsync(Guid surveyId);
        Task AddResponseAsync(SurveyResponseDto responseDto);

        Task<IEnumerable<SurveyModel>> GetSurveysCreatedByUserAsync(Guid userId);
        Task<int> GetResponseCountAsync(Guid surveyId);
        Task<SurveyModel?> GetSurveyByIdAsync(Guid surveyId);
    }
}
