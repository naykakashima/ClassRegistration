using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetSurveyBySubjectIdUseCase
    {
        private readonly ISubjectSurveyService _surveyService;

        public GetSurveyBySubjectIdUseCase(ISubjectSurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public async Task<SubjectSurveyDto?> ExecuteAsync(Guid subjectId)
        {
            return await _surveyService.GetBySubjectIdAsync(subjectId);
        }
    }
}
