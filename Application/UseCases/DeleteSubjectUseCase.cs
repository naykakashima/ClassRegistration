using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class DeleteSubjectUseCase
    {
        private readonly ISubjectRepository _repo;
        private readonly IDbContextFactory<AppDbContext> _factory;

        public DeleteSubjectUseCase(ISubjectRepository repo, IDbContextFactory<AppDbContext> factory)
        {
            _repo = repo;
            _factory = factory;
        }

        public async Task ExecuteAsync(Guid subjectId, CancellationToken ct = default)
        {
            await using var db = await _factory.CreateDbContextAsync(ct);
            var subject = await _repo.GetByIdAsync(subjectId);
            if (subject == null)
                throw new InvalidOperationException("Subject not found");

            if (subject.Classes.Any())
                throw new InvalidOperationException("Cannot delete a subject with existing classes");

            await _repo.DeleteAsync(subject, db, ct);
        }
    }
}
