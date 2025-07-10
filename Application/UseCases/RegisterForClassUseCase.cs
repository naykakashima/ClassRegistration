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

        //public async Task<Result> ExecuteAsync(Guid classId, ClaimsPrincipal userPrincipal)
        //{
        //    var user = await _userService.GetOrCreateCurrentUserAsync(userPrincipal);
        //    if (user == null)
        //        return Result.Failure("User not recognized.");

        //    var cls = await _classRepository.GetByIdAsync(classId);
        //    if (cls == null)
        //        return Result.Failure("Class not found.");

        //    var alreadyRegistered = await _registrationRepository.ExistsAsync(classId, user.Id);
        //    if (alreadyRegistered)
        //        return Result.Failure("You are already registered for this class.");

        //    await _registrationRepository.RegisterUserAsync(user.Id, classId);
        //    return Result.Success();
        //}
    }
}
