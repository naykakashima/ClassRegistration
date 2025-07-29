using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetAllSubjectsUseCase
    {
        private readonly ISubjectRepository _repo;
        public GetAllSubjectsUseCase(ISubjectRepository repo) => _repo = repo;

        public async Task<List<SubjectSummaryDto>> ExecuteAsync()
        {
            var subjects = await _repo.GetAllAsync();
            return subjects.Select(s => new SubjectSummaryDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                ClassCount = s.Classes.Count
            }).ToList();
        }
    }
}
