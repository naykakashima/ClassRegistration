using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly AppDbContext _db;

        public SurveyRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(SurveyBase newSurvey, CancellationToken ct)
        {
            _db.Surveys.Add(newSurvey);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<List<SurveyBase>> GetAllAsync()
        {
            return await _db.Surveys.ToListAsync();
        }

        public async Task<SurveyBase?> GetByIdAsync(Guid surveyId)
        {
            return await _db.Surveys.FindAsync(surveyId);
        }

        public async Task UpdateAsync(SurveyBase updatedSurvey, CancellationToken ct)
        {
            var existing = await _db.Surveys
                .FirstOrDefaultAsync(s => s.Id == updatedSurvey.Id, ct); 

            if (existing == null)
                throw new Exception("Survey not found.");

            // Handle patching based on actual type
            existing.Title = updatedSurvey.Title;
            existing.Description = updatedSurvey.Description;
            existing.JsonDefinition = updatedSurvey.JsonDefinition;

            switch (existing)
            {
                case SubjectSurvey subject when updatedSurvey is SubjectSurvey updatedSubject:
                    subject.SubjectId = updatedSubject.SubjectId;
                    break;

                case ClassSurvey cls when updatedSurvey is ClassSurvey updatedClass:
                    cls.ClassId = updatedClass.ClassId;
                    break;

                default:
                    throw new InvalidOperationException("Survey type mismatch or unsupported type.");
            }

            await _db.SaveChangesAsync(ct);
        }


        public async Task DeleteAsync(Guid surveyId, CancellationToken ct)
        {
            var surveyToDelete = await _db.Surveys.FindAsync(surveyId);
            if (surveyToDelete != null)
            {
                _db.Surveys.Remove(surveyToDelete);
                await _db.SaveChangesAsync(ct);
            }
        }

        public async Task<List<SubjectSurvey>> GetAllSubjectSurveysAsync()
        {
            return await _db.Surveys
                .OfType<SubjectSurvey>()
                .Include(s => s.Subject)
                .ToListAsync();
        }

        public async Task<SubjectSurvey?> GetBySubjectIdAsync(Guid subjectId)
        {
            return await _db.Surveys
                .OfType<SubjectSurvey>()
                .FirstOrDefaultAsync(s => s.SubjectId == subjectId);
        }

        public async Task<List<ClassSurvey>> GetAllClassSurveysAsync()
        {
            return await _db.Surveys
                .OfType<ClassSurvey>()
                .Include(s => s.Class)
                .ToListAsync();
        }

        public async Task<ClassSurvey?> GetByClassIdAsync(Guid classId)
        {
            return await _db.Surveys
                .OfType<ClassSurvey>()
                .FirstOrDefaultAsync(s => s.ClassId == classId);
        }
    }

}
