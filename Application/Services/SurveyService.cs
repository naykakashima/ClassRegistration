using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly AppDbContext _context;

        public SurveyService(ISurveyRepository surveyRepository, AppDbContext context)
        {
            _surveyRepository = surveyRepository;
            _context = context;
        }

        public async Task<List<SurveyDto>> GetAllAsync()
        {
            var surveys = await _surveyRepository.GetAllAsync();
            return surveys.ConvertAll(MapToDto);
        }

        public async Task<SurveyDto?> GetByIdAsync(Guid id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id);
            return survey == null ? null : MapToDto(survey);
        }

        public async Task<SurveyDto?> GetBySubjectIdAsync(Guid subjectId)
        {
            var survey = await _surveyRepository.GetBySubjectIdAsync(subjectId);
            return survey == null ? null : MapToDto(survey);
        }

        public async Task AddAsync(SurveyDto dto)
        {
            var survey = MapToEntity(dto);
            await _surveyRepository.AddAsync(survey, _context, CancellationToken.None);
        }

        public async Task UpdateAsync(SurveyDto dto)
        {
            var survey = MapToEntity(dto);
            await _surveyRepository.UpdateAsync(survey, _context, CancellationToken.None);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _surveyRepository.DeleteAsync(id, _context, CancellationToken.None);
        }

        // Mapping helpers

        private SurveyDto MapToDto(Survey entity)
        {
            return new SurveyDto
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

        private Survey MapToEntity(SurveyDto dto)
        {
            return new Survey
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
