// RegisterForClassUseCase.cs
using ClassRegistrationApplication2025.Application.Common;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using ClassRegistrationApplication2025.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<Result> ExecuteAsync(Guid classId, string adUserId, string Name, string EmailSMTP, CancellationToken ct = default)
        {
            var user = await _userService.GetUserByAdUserIdAsync(adUserId, ct);
            if (user == null)
                return Result.Failure("User not recognized.");

            var cls = await _classRepository.GetClassWithRegistrationsAsync(classId, ct);
            if (cls == null)
                return Result.Failure("Class not found.");

            if (cls.Status != ClassStatus.Open)
                return Result.Failure("Class is not open for registration.");

            var alreadyRegistered = await _registrationRepository.ExistsAsync(classId, user.Id);
            if (alreadyRegistered)
                return Result.Failure("You are already registered for this class.");

            var registrationCount = await _registrationRepository.GetRegistrationCountByClassAsync(classId);
            if (registrationCount >= cls.MaxSlots)
                return Result.Failure("Class is full.");

            await _registrationRepository.RegisterUserAsync(user.Id, classId, Name, EmailSMTP, ct);

            return Result.Success();
        }
    }
}
