using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class CreateClassUseCase
    {
        private readonly IClassRepository _classRepo;
        private readonly IUserService _userService;
        private readonly ISubjectRepository _subjectRepo;

        public CreateClassUseCase(
            IClassRepository classRepo,
            IUserService userService,
            ISubjectRepository subjectRepo)
        {
            _classRepo = classRepo;
            _userService = userService;
            _subjectRepo = subjectRepo;
        }

        public async Task ExecuteAsync(CreateClassDto dto, string adUserId, CancellationToken ct = default)
        {
            // Get user via UserService (which should also be using DbContextFactory)
            var user = await _userService.GetUserByAdUserIdAsync(adUserId, ct);
            if (user == null)
                throw new Exception("User not found");

            if (user.Role == Role.User)
                throw new UnauthorizedAccessException("You are not allowed to create classes, please contact an admin");

            if (dto.Date == null || dto.StartTime == null || dto.EndTime == null)
                throw new ArgumentException("Date and time fields are required.");

            // Get subject via SubjectRepository
            var subject = await _subjectRepo.GetByIdAsync(dto.SubjectId, ct);
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
                SubjectId = subject.Id
                // Note: We don't set navigation properties here
            };

            // Use the standard AddAsync method
            await _classRepo.AddAsync(newClass, ct);
        }
    }
}