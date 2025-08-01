using ClassRegistrationApplication2025.Application.DTOs;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface IGetAvailableClassSurveysForUserUseCase
    {
        Task<List<ClassSurveyDto>> ExecuteAsync(Guid userId, CancellationToken ct);
    }
}
