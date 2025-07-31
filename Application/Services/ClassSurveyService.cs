using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.Services
{
    public class ClassSurveyService : IClassSurveyService
    {
        private readonly ISurveyRepository _surveyRepository;

        public ClassSurveyService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<List<ClassSurveyDto>> GetAllAsync()
        {
            var surveys = await _surveyRepository.GetAllClassSurveysAsync();
            return surveys.ConvertAll(MapToDto);
        }

        public async Task<ClassSurveyDto?> GetByIdAsync(Guid id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id);
            return survey is ClassSurvey classSurvey ? MapToDto(classSurvey) : null;
        }


        public async Task<ClassSurveyDto?> GetByClassIdAsync(Guid subjectId)
        {
            var survey = await _surveyRepository.GetByClassIdAsync(subjectId);
            return survey == null ? null : MapToDto(survey);
        }

        public async Task AddAsync(ClassSurveyDto dto)
        {
            var survey = MapToEntity(dto);
            await _surveyRepository.AddAsync(survey, CancellationToken.None);
        }

        public async Task UpdateAsync(ClassSurveyDto dto)
        {
            var survey = MapToEntity(dto);
            await _surveyRepository.UpdateAsync(survey, CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _surveyRepository.DeleteAsync(id, CancellationToken.None);
        }

        // Mapping helpers

        private ClassSurveyDto MapToDto(ClassSurvey entity)
        {
            return new ClassSurveyDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                JsonDefinition = entity.JsonDefinition,
                CreatedByUserId = entity.CreatedByUserId,
                CreatedAt = entity.CreatedAt,
                ClassId = entity.ClassId
            };
        }


        private SurveyBase MapToEntity(ClassSurveyDto dto)
        {
            return new ClassSurvey
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                JsonDefinition = dto.JsonDefinition,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt,
                ClassId = dto.ClassId
            };
        }
    }
}
