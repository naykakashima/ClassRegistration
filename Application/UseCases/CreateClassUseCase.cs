using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class CreateClassUseCase
    {
        private readonly IClassRepository _classRepo;
        private readonly IUserService _userService;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public CreateClassUseCase(IClassRepository classRepo, IUserService userService, IDbContextFactory<AppDbContext> contextFactory)
        {
            _classRepo = classRepo;
            _userService = userService;
            _contextFactory = contextFactory;
        }

        public async Task ExecuteAsync(CreateClassDto dto, string adUserId, CancellationToken ct = default)
        {
            using var context = _contextFactory.CreateDbContext();

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserID == adUserId);
            if (user == null)
                throw new Exception("User not found");

            if (user.Role == Role.User)
                throw new UnauthorizedAccessException("You are not allowed to create classes, please contact an admin");

            if (dto.Date == null || dto.StartTime == null || dto.EndTime == null)
                throw new ArgumentException("Date and time fields are required.");

            var subject = await context.Subjects.FirstOrDefaultAsync(s => s.Id == dto.SubjectId, ct);
            if (subject == null)
                throw new Exception("Subject not found");

            var newClass = new Class
            {
                Id = Guid.NewGuid(),
                ClassName = dto.ClassName,
                SessionName = dto.SessionName,
                Presenter = dto.PresenterName,
                Date = dto.Date.Value,
                StartTime = dto.StartTime.Value,
                EndTime = dto.EndTime.Value,
                MaxSlots = dto.MaxSlots,
                Status = ClassStatus.Open,
                CreatedByUserId = user.Id,
                CreatedByUser = user,
                SubjectId = subject.Id,
                Subject = subject
            };

            await _classRepo.AddAsync(newClass, context, ct);
        }
    }
}
