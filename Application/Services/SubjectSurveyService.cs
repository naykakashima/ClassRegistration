using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.Services
{
    public class SubjectSurveyService : ISubjectSurveyService
    {
        private readonly ISurveyRepository _surveyRepository;

        public SubjectSurveyService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<List<SubjectSurveyDto>> GetAllAsync()
        {
            var surveys = await _surveyRepository.GetAllSubjectSurveysAsync();
            return surveys.ConvertAll(MapToDto);
        }

        public async Task<SubjectSurveyDto?> GetByIdAsync(Guid id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id);
            return survey is SubjectSurvey subjectSurvey ? MapToDto(subjectSurvey) : null;
        }

        public async Task<SubjectSurveyDto?> GetBySubjectIdAsync(Guid subjectId)
        {
            var survey = await _surveyRepository.GetBySubjectIdAsync(subjectId);
            return survey == null ? null : MapToDto(survey);
        }

        public async Task AddAsync(SubjectSurveyDto dto)
        {
            var survey = MapToEntity(dto);
            await _surveyRepository.AddAsync(survey, CancellationToken.None);
        }

        public async Task UpdateAsync(SubjectSurveyDto dto)
        {
            var survey = MapToEntity(dto);
            await _surveyRepository.UpdateAsync(survey, CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _surveyRepository.DeleteAsync(id, CancellationToken.None);
        }

        // Mapping helpers

        private SubjectSurveyDto MapToDto(SubjectSurvey entity)
        {
            return new SubjectSurveyDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                JsonDefinition = entity.JsonDefinition,
                CreatedByUserId = entity.CreatedByUserId,
                CreatedAt = entity.CreatedAt,
                SubjectId = entity.SubjectId
            };
        }

        private SurveyBase MapToEntity(SubjectSurveyDto dto)
        {
            return new SubjectSurvey
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                JsonDefinition = dto.JsonDefinition,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt,
                SubjectId = dto.SubjectId
            };
        }
    }
}
