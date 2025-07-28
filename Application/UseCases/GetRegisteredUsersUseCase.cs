using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetRegisteredUsersUseCase
    {
        private readonly IRegistrationRepository _registrationRepository;

        public GetRegisteredUsersUseCase(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<List<RegisteredUsersDto>> ExecuteAsync(Guid classId)
        {
            var registrations = await _registrationRepository.GetRegistrationsByClassAsync(classId);

            return registrations.Select(r => new RegisteredUsersDto
            {
                Id = r.Id,
                UserID = r.User.UserID,
                UserName = r.UserName,
                RegisteredAt = r.RegisteredAt
            }).ToList();
        }
    }
}
