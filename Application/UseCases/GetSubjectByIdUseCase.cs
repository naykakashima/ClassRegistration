using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetSubjectByIdUseCase
    {
        private readonly ISubjectRepository _repo;
        private readonly IDbContextFactory<AppDbContext> _factory;

        public GetSubjectByIdUseCase(ISubjectRepository repo, IDbContextFactory<AppDbContext> factory)
        {
            _repo = repo;
            _factory = factory;
        }

        public async Task<CreateSubjectDto?> ExecuteAsync(Guid subjectId, CancellationToken ct = default)
        {
            using var db = _factory.CreateDbContext();

            var subject = await _repo.GetByIdAsync(subjectId);

            if (subject == null) return null;

            return new CreateSubjectDto
            {
                Title = subject.Title,
                Description = subject.Description
            };
        }
    }
}
