using ClassRegistrationApplication2025.Application.Common;
using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Application.UseCases
{
    public class GetClassDetailsUseCase
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public GetClassDetailsUseCase(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Result<ClassDetailDto>> ExecuteAsync(Guid classId, CancellationToken ct = default)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync(ct);
                var classEntity = await context.Classes
                    .Include(c => c.Registrations)
                    .FirstOrDefaultAsync(c => c.Id == classId, ct);

                if (classEntity == null)
                {
                    return Result<ClassDetailDto>.Failure("Class not found");
                }

                return Result<ClassDetailDto>.Success(new ClassDetailDto
                {
                    Id = classEntity.Id,
                    ClassName = classEntity.ClassName,
                    SessionName = classEntity.SessionName,
                    Presenter = classEntity.Presenter,
                    Date = classEntity.Date,
                    StartTime = classEntity.StartTime,
                    EndTime = classEntity.EndTime,
                    MaxSlots = classEntity.MaxSlots,
                    Status = classEntity.Status,
                    RegisteredCount = classEntity.Registrations.Count,
                    CreatedByUserId = classEntity.CreatedByUserId,
                    SubjectId = classEntity.SubjectId
                });
            }
            catch (Exception ex)
            {
                return Result<ClassDetailDto>.Failure($"Error retrieving class: {ex.Message}");
            }
        }
    }
}