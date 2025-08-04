using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using SurveyBuilder.Models;
using System.Text.Json;

namespace ClassRegistrationApplication2025.Application.Services
{
    public class SurveyQueryService : ISurveyQueryService
    {
        private readonly ISurveyRepository _repo;
        private readonly IGetSurveyBaseByIdUseCase _getSurveyBase;

        public SurveyQueryService(ISurveyRepository repo, IGetSurveyBaseByIdUseCase getSurveyBase)
        {
            _repo = repo;
            _getSurveyBase = getSurveyBase;
        }

        public async Task<SurveyModel?> GetSurveyModelByIdAsync(Guid surveyId)
        {
            var surveyBase = await _getSurveyBase.ExecuteAsync(surveyId, CancellationToken.None);

            if (surveyBase == null)
            {
                Console.WriteLine("SurveyBase was null");
                return null;
            }

            Console.WriteLine("Survey JSON: " + surveyBase.JsonDefinition);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<SurveyModel>(surveyBase.JsonDefinition, options);
        }

    }

}
