using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class DeleteClassUseCase
    {
        private readonly IClassRepository _classRepo;
        private readonly IUserService _userService;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public DeleteClassUseCase(IClassRepository classRepo, IUserService userService, IDbContextFactory<AppDbContext> contextFactory)
        {
            _classRepo = classRepo;
            _userService = userService;
            _contextFactory = contextFactory;
        }

        public async Task ExecuteAsync(Guid classId, CreateClassDto dto, CancellationToken ct = default)
        {
            using var context = _contextFactory.CreateDbContext();

            await _classRepo.DeleteClassAsync(classId, context, ct);
        }
    }
}
