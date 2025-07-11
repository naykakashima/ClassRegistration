using ClassRegistrationApplication2025.Application.Common;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class RegisterForClassUseCase
    {
        private readonly IClassRepository _classRepository;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IUserService _userService;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public RegisterForClassUseCase(
            IClassRepository classRepository,
            IRegistrationRepository registrationRepository,
            IUserService userService,
            IDbContextFactory<AppDbContext> contextFactory)
        {
            _classRepository = classRepository;
            _registrationRepository = registrationRepository;
            _userService = userService;
            _contextFactory = contextFactory;
        }

        public async Task<Result> ExecuteAsync(Guid classId, string adUserId, CancellationToken ct = default)
        {
            using var context = _contextFactory.CreateDbContext();

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserID == adUserId, ct);
            if (user == null)
                return Result.Failure("User not recognized.");

            var cls = await context.Classes.Include(c => c.Registrations).FirstOrDefaultAsync(c => c.Id == classId, ct);
            if (cls == null)
                return Result.Failure("Class not found.");

            if (cls.Status != ClassStatus.Open)
                return Result.Failure("Class is not open for registration.");

            bool alreadyRegistered = await context.Registrations.AnyAsync(r => r.ClassId == classId && r.UserId == user.Id, ct);
            if (alreadyRegistered)
                return Result.Failure("You are already registered for this class.");

            if (cls.Registrations.Count >= cls.MaxSlots)
                return Result.Failure("Class is full.");

            var registration = new Registration
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ClassId = classId,
                RegisteredAt = DateTime.UtcNow
            };

            context.Registrations.Add(registration);
            await context.SaveChangesAsync(ct);

            return Result.Success();
        }
    }

}
