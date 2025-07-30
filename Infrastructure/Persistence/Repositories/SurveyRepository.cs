using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{

    namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
    {
        public class SurveyRepository : ISurveyRepository
        {
            private readonly AppDbContext _db;

            public SurveyRepository(AppDbContext db)
            {
                _db = db;
            }

            public async Task AddAsync(Survey newSurvey, AppDbContext context, CancellationToken ct)
            {
                context.Surveys.Add(newSurvey);
                await context.SaveChangesAsync(ct);
            }

            public async Task<List<Survey>> GetAllAsync()
            {
                return await _db.Surveys.ToListAsync();
            }

            public async Task<Survey?> GetByIdAsync(Guid surveyId)
            {
                return await _db.Surveys.FindAsync(surveyId);
            }

            public async Task<Survey?> GetBySubjectIdAsync(Guid subjectId)
            {
                return await _db.Surveys.FirstOrDefaultAsync(s => s.Subject != null && s.Subject.Id == subjectId);
                // SubjectId foreign key property in Survey, do:
                // return await _db.Surveys.FirstOrDefaultAsync(s => s.SubjectId == subjectId);
            }

            public async Task UpdateAsync(Survey survey, AppDbContext context, CancellationToken ct)
            {
                context.Surveys.Update(survey);
                await context.SaveChangesAsync(ct);
            }

            public async Task DeleteAsync(Guid surveyId, AppDbContext context, CancellationToken ct)
            {
                var surveyToDelete = await context.Surveys.FindAsync(surveyId);
                if (surveyToDelete != null)
                {
                    context.Surveys.Remove(surveyToDelete);
                    await context.SaveChangesAsync(ct);
                }
            }
        }
    }

}
