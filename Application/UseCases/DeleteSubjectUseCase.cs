using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class DeleteSubjectUseCase
    {
        private readonly ISubjectRepository _repo;

        public DeleteSubjectUseCase(ISubjectRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid subjectId, CancellationToken ct = default)
        {
            var subject = await _repo.GetByIdAsync(subjectId, ct);
            if (subject == null)
                throw new InvalidOperationException("Subject not found");

            if (subject.Classes?.Any() == true)
                throw new InvalidOperationException("Cannot delete a subject with existing classes");

            await _repo.DeleteAsync(subject, ct);
        }
    }
}