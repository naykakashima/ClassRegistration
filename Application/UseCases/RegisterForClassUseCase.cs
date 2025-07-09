using ClassRegistrationApplication2025.Application.Common;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System.Security.Claims;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class RegisterForClassUseCase
    {
        private readonly IClassRepository _classRepository;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IUserService _userService;

        public RegisterForClassUseCase(
            IClassRepository classRepository,
            IRegistrationRepository registrationRepository,
            IUserService userService)
        {
            _classRepository = classRepository;
            _registrationRepository = registrationRepository;
            _userService = userService;
        }

        public async Task<Result> ExecuteAsync(Guid classId, ClaimsPrincipal user)
        {
            var currentUser = await _userService.GetOrCreateCurrentUserAsync(user);
            var classEntity = await _classRepository.GetByIdAsync(classId);

            if (classEntity == null)
                return Result.Failure("Class not found");

            if (classEntity.Status != ClassStatus.Open)
                return Result.Failure("Registration closed");

            if (await _registrationRepository.ExistsAsync(classId, currentUser.Id))
                return Result.Failure("Already registered");

            var currentCount = await _registrationRepository.GetCountForClassAsync(classId);
            if (currentCount >= classEntity.MaxSlots)
                return Result.Failure("Class full");

            var registration = new Registration
            {
                ClassId = classId,
                UserId = currentUser.Id,
                UserName = currentUser.Name,
                RegisteredAt = DateTime.UtcNow
            };

            await _registrationRepository.AddAsync(registration);
            return Result.Success();
        }
    }
}
