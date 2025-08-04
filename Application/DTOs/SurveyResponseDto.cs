namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class SurveyResponseDto
    {
        public Guid Id { get; set; }
        public Guid SurveyId { get; set; }
        public Guid UserId { get; set; }
        public string JsonAnswers { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
