using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Domain.Enums;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class DeleteClassUseCase
    {
        private readonly IClassRepository _classRepo;
        private readonly IUserService _userService;
        private readonly IRegistrationRepository _registrationRepo;
        private readonly ISurveyRepository _surveyRepo;

        public DeleteClassUseCase(
            IClassRepository classRepo,
            IUserService userService,
            IRegistrationRepository registrationRepo,
            ISurveyRepository surveyRepository)
        {
            _classRepo = classRepo;
            _userService = userService;
            _registrationRepo = registrationRepo;
            _surveyRepo = surveyRepository;
        }

        public async Task ExecuteAsync(Guid classId, string currentUserId, CancellationToken ct = default)
        {
            // 1. Verify user permissions
            var currentUser = await _userService.GetUserByAdUserIdAsync(currentUserId, ct);
            if (currentUser?.Role < Role.ClassManager)
            {
                throw new UnauthorizedAccessException("Only class managers can delete classes");
            }

            // 2. Check if class has existing registrations
            var registrationClass = await _registrationRepo.GetRegistrationsByClassAsync(classId);
            var registrationCount = registrationClass.Count();
            if (registrationCount > 0)
            {
                throw new InvalidOperationException("Cannot delete class with existing registrations");
            }

            var classHasSurvey = await _surveyRepo.GetByClassIdAsync(classId);
            if (classHasSurvey?.ClassId != null)
            {
                throw new InvalidOperationException("Cannot delete class with existing survey");
            }

                // 3. Perform deletion
            await _classRepo.DeleteClassAsync(classId, ct);
        }
    }
}