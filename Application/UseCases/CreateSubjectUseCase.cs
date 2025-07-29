using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class CreateSubjectUseCase
    {
        private readonly ISubjectRepository _repo;
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CreateSubjectUseCase(ISubjectRepository repo, IDbContextFactory<AppDbContext> factory)
        {
            _repo = repo;
            _factory = factory;
        }

        public async Task ExecuteAsync(CreateSubjectDto dto, CancellationToken ct = default)
        {
            using var db = _factory.CreateDbContext();

            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Classes = new List<Class>()
            };

            await _repo.AddAsync(subject, db, ct);
        }
    }

}
