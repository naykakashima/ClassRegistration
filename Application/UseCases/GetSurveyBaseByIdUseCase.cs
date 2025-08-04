using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetSurveyBaseByIdUseCase : IGetSurveyBaseByIdUseCase
    {
        private readonly ISurveyRepository _repository;

        public GetSurveyBaseByIdUseCase(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public async Task<SurveyBase?> ExecuteAsync(Guid id, CancellationToken ct)
        {
            return await _repository.GetByIdAsync(id);
        }
    }

}
