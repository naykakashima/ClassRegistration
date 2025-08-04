using ClassRegistrationApplication2025.Domain.Entities;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories
{
    public class SurveyResponseRepository : ISurveyResponseRepository
    {
        private readonly AppDbContext _db;

        public SurveyResponseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<SurveyResponse?> GetBySurveyAndUserAsync(Guid surveyId, Guid userId)
        {
            return await _db.SurveyResponses
                .FirstOrDefaultAsync(r => r.SurveyId == surveyId && r.UserId == userId);
        }

        public async Task<List<SurveyResponse>> GetBySurveyIdAsync(Guid surveyId)
        {
            return await _db.SurveyResponses
                .Where(r => r.SurveyId == surveyId)
                .ToListAsync();
        }

        public async Task AddAsync(SurveyResponse response)
        {
            _db.SurveyResponses.Add(response);
            await _db.SaveChangesAsync();
        }
    }
}
