using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class UpdateSubjectUseCase
    {
        private readonly ISubjectRepository _repo;

        public UpdateSubjectUseCase(ISubjectRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid subjectId, CreateSubjectDto dto, CancellationToken ct = default)
        {
            var subject = await _repo.GetByIdAsync(subjectId, ct);
            if (subject == null)
                throw new Exception("Subject not found.");

            subject.Title = dto.Title;
            subject.Description = dto.Description;

            await _repo.UpdateAsync(subject, ct);
        }
    }
}