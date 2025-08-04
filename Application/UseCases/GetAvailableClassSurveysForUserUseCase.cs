using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System.Linq;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetAvailableClassSurveysForUserUseCase : IGetAvailableClassSurveysForUserUseCase
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IClassRepository _classRepository; 
        private readonly IRegistrationRepository _registrationRepository;
        // TODO: Add ISurveyResponseRepository when you wire up response tracking

        public GetAvailableClassSurveysForUserUseCase(
            ISurveyRepository surveyRepository,
            IClassRepository classRepository,
            IRegistrationRepository registrationRepository)
        {
            _surveyRepository = surveyRepository;
            _classRepository = classRepository;
            _registrationRepository = registrationRepository;
        }

        public async Task<List<ClassSurveyDto>> ExecuteAsync(Guid userId, CancellationToken ct)
        {
            // Get all surveys and registrations in parallel with separate contexts
            var surveysTask = _surveyRepository.GetAllClassSurveysAsync();
            var registrationsTask = _registrationRepository.GetRegistrationsByUserIdAsync(userId, ct);

            await Task.WhenAll(surveysTask, registrationsTask);

            var allSurveys = await surveysTask;
            var registrations = await registrationsTask;

            // Get attended class IDs
            var attendedClassIds = registrations
                .Where(r => r.AttendedAt != null)
                .Select(r => r.ClassId)
                .Distinct()
                .ToList();

            if (!attendedClassIds.Any())
            {
                return new List<ClassSurveyDto>();
            }

            // Get class information for attended classes
            var attendedClasses = await _classRepository.GetClassesByIdsAsync(attendedClassIds, ct);

            return allSurveys
                .Where(survey => attendedClassIds.Contains(survey.ClassId))
                .Select(survey => new ClassSurveyDto
                {
                    Id = survey.Id,
                    ClassId = survey.ClassId,
                    Title = survey.Title,
                    Description = survey.Description,
                    CreatedAt = survey.CreatedAt
                })
                .ToList();
        }
    }

}
