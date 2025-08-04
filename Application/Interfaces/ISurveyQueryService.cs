using ClassRegistrationApplication2025.Domain.Entities;
using SurveyBuilder.Models;

namespace ClassRegistrationApplication2025.Application.Interfaces
{
    public interface ISurveyQueryService
    {
        Task<SurveyModel?> GetSurveyModelByIdAsync(Guid id);
    }

}
