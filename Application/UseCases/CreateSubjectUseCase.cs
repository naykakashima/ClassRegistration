using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class CreateSubjectUseCase
    {
        private readonly ISubjectRepository _repo;

        public CreateSubjectUseCase(ISubjectRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(CreateSubjectDto dto, CancellationToken ct = default)
        {
            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Classes = new List<Class>()
            };

            await _repo.AddAsync(subject, ct);
        }
    }
}