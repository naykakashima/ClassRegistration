using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class UpdateSubjectUseCase
    {
        private readonly ISubjectRepository _repo;
        private readonly IDbContextFactory<AppDbContext> _factory;

        public UpdateSubjectUseCase(ISubjectRepository repo, IDbContextFactory<AppDbContext> factory)
        {
            _repo = repo;
            _factory = factory;
        }

        public async Task ExecuteAsync(Guid subjectId, CreateSubjectDto dto, CancellationToken ct = default)
        {
            using var db = _factory.CreateDbContext();

            var subject = await _repo.GetByIdAsync(subjectId);
            if (subject == null)
                throw new Exception("Subject not found.");

            subject.Title = dto.Title;
            subject.Description = dto.Description;

            await _repo.UpdateAsync(subject, db, ct);
        }
    }
}
