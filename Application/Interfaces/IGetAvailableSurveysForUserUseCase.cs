using ClassRegistrationApplication2025.Application.DTOs;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface IGetAvailableSubjectSurveysForUserUseCase
    {
        Task<List<SubjectSurveyDto>> ExecuteAsync(Guid userId, CancellationToken ct);
    }
}

