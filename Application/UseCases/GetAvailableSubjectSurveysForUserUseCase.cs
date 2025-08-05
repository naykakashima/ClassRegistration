using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System.Linq;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetAvailableSubjectSurveysForUserUseCase : IGetAvailableSubjectSurveysForUserUseCase
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IClassRepository _classRepository;

        public GetAvailableSubjectSurveysForUserUseCase(
            ISurveyRepository surveyRepository,
            IRegistrationRepository registrationRepository,
            IClassRepository classRepository)
        {
            _surveyRepository = surveyRepository;
            _registrationRepository = registrationRepository;
            _classRepository = classRepository;
        }

        public async Task<List<SubjectSurveyDto>> ExecuteAsync(Guid userId, CancellationToken ct)
        {
            // Step 1: Get all user registrations
            var registrations = await _registrationRepository.GetRegistrationsByUserIdAsync(userId, ct);

            // Step 2: Get classes one by one using the stable GetByIdAsync
            var classes = new List<Class>();
            foreach (var registration in registrations)
            {
                var classEntity = await _classRepository.GetByIdAsync(registration.ClassId, ct);
                if (classEntity != null)
                {
                    classes.Add(classEntity);
                }
            }

            if (!classes.Any())
                return new List<SubjectSurveyDto>();

            var subjectIds = classes.Select(c => c.SubjectId).Distinct().ToList();

            // Rest of your existing code...
            var releasedSurveys = await _surveyRepository.GetReleasedSubjectSurveysAsync();

            var availableSurveys = releasedSurveys
                .Where(s => s.SubjectId.HasValue && subjectIds.Contains(s.SubjectId.Value))
                .Select(s => new SubjectSurveyDto
                {
                    Id = s.Id,
                    SubjectId = s.SubjectId,
                    Title = s.Title,
                    Description = s.Description,
                    JsonDefinition = s.JsonDefinition,
                    CreatedByUserId = s.CreatedByUserId,
                    CreatedAt = s.CreatedAt,
                    IsReleased = s.IsReleased
                })
                .ToList();

            return availableSurveys;
        }
    }

}
