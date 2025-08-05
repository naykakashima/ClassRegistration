using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using SurveyBuilder.Models;
using System.Text.Json;

namespace ClassRegistrationApplication2025.Application.Services
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly ISurveyResponseRepository _repository;
        private readonly ISurveyRepository _SurveyRepository;

        public SurveyResponseService(ISurveyResponseRepository repository, ISurveyRepository surveyRepository)
        {
            _repository = repository;
            _SurveyRepository = surveyRepository;
        }

        public async Task<SurveyResponseDto?> GetResponseAsync(Guid surveyId, Guid userId)
        {
            var entity = await _repository.GetBySurveyAndUserAsync(surveyId, userId);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<SurveyResponseDto>> GetResponsesBySurveyIdAsync(Guid surveyId)
        {
            var entities = await _repository.GetBySurveyIdAsync(surveyId);
            return entities.Select(MapToDto).ToList();
        }

        public async Task AddResponseAsync(SurveyResponseDto dto)
        {
            // Guard against duplicate submissions
            var existing = await _repository.GetBySurveyAndUserAsync(dto.SurveyId, dto.UserId);
            if (existing != null)
                throw new InvalidOperationException("User has already submitted this survey.");

            var entity = MapToEntity(dto);
            await _repository.AddAsync(entity);
        }

        public async Task<IEnumerable<SurveyModel>> GetSurveysCreatedByUserAsync(Guid userId)
        {
            var surveys = await _SurveyRepository.GetByCreatorUserIdAsync(userId);
            return surveys.Select(MapToModel);
        }


        public async Task<int> GetResponseCountAsync(Guid surveyId)
        {
            var responses = await _repository.GetBySurveyIdAsync(surveyId);
            return responses.Count;
        }

        // Mapping
        private SurveyResponseDto MapToDto(SurveyResponse entity) => new()
        {
            Id = entity.Id,
            SurveyId = entity.SurveyId,
            UserId = entity.UserId,
            JsonAnswers = entity.JsonAnswers,
            SubmittedAt = entity.SubmittedAt
        };

        private SurveyResponse MapToEntity(SurveyResponseDto dto) => new()
        {
            Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
            SurveyId = dto.SurveyId,
            UserId = dto.UserId,
            JsonAnswers = dto.JsonAnswers,
            SubmittedAt = dto.SubmittedAt
        };

        private SurveyModel MapToModel(SurveyBase survey)
        {
            var model = new SurveyModel
            {
                Id = survey.Id.ToString(),
                Title = survey.Title,
                Description = survey.Description,
                Questions = new List<SurveyQuestionModel>()
            };

            if (!string.IsNullOrEmpty(survey.JsonDefinition))
            {
                try
                {
                    // Deserialize JSON string of questions into List<SurveyQuestionModel>
                    model.Questions = JsonSerializer.Deserialize<List<SurveyQuestionModel>>(survey.JsonDefinition)
                                      ?? new List<SurveyQuestionModel>();
                }
                catch (JsonException)
                {
                    // Handle invalid JSON gracefully; maybe log or leave Questions empty
                    model.Questions = new List<SurveyQuestionModel>();
                }
            }

            return model;
        }

        public async Task<SurveyModel?> GetSurveyByIdAsync(Guid surveyId)
        {
            var survey = await _SurveyRepository.GetByIdAsync(surveyId);
            return survey == null ? null : MapToModel(survey);
        }

    }

}
