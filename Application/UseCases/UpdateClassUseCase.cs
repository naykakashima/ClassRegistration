using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Common;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class UpdateClassUseCase
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public UpdateClassUseCase(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Result> ExecuteAsync(Guid classId, CreateClassDto dto, CancellationToken ct = default)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync(ct);

                var classEntity = await context.Classes.FirstOrDefaultAsync(c => c.Id == classId, ct);

                if (classEntity is null)
                {
                    return Result.Failure("Class not found.");
                }

                // Update fields
                classEntity.ClassName = dto.ClassName;
                classEntity.SessionName = dto.SessionName;
                classEntity.Presenter = dto.PresenterName;
                classEntity.Date = dto.Date ?? classEntity.Date;
                classEntity.StartTime = dto.StartTime ?? classEntity.StartTime;
                classEntity.EndTime = dto.EndTime ?? classEntity.EndTime;
                classEntity.MaxSlots = dto.MaxSlots;

                // Save changes
                await context.SaveChangesAsync(ct);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Update failed: {ex.Message}");
            }
        }
    }
}
