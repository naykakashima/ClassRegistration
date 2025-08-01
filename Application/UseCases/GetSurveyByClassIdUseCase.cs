using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetSurveyByClassIdUseCase
    {
        private readonly IClassSurveyService _surveyService;

        public GetSurveyByClassIdUseCase(IClassSurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public async Task<ClassSurveyDto?> ExecuteAsync(Guid subjectId)
        {
            return await _surveyService.GetByClassIdAsync(subjectId);
        }
    }
}
