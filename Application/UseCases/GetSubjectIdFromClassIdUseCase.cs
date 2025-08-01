using ClassRegistrationApplication2025.Application.Common;
using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetSubjectIdFromClassIdUseCase
    {
        private readonly IClassRepository _classRepo;

        public GetSubjectIdFromClassIdUseCase(IClassRepository classRepo)
        {
            _classRepo = classRepo;
        }

        public async Task<Guid?> ExecuteAsync(Guid classId)
        {
            return await _classRepo.GetSubjectIdByClassIdAsync(classId);
        }
    }
}
