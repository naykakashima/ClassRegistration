namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class SurveyResponse
    {
        public Guid Id { get; set; }

        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string JsonAnswers { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
