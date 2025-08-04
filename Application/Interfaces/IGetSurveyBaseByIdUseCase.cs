using ClassRegistrationApplication2025.Domain.Entities;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface IGetSurveyBaseByIdUseCase
    {
        Task<SurveyBase?> ExecuteAsync(Guid id, CancellationToken ct);
    }
}
