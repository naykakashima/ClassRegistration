namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class SurveyDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string JsonDefinition { get; set; } = string.Empty;

        public Guid CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid? SubjectId { get; set; }
    }
}
