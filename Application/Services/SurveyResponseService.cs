using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.Services
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly ISurveyResponseRepository _repository;

        public SurveyResponseService(ISurveyResponseRepository repository)
        {
            _repository = repository;
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
    }

}
