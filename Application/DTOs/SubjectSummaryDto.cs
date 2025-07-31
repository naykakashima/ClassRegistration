namespace ClassRegistrationApplication2025.Application.DTOs
{
    public class SubjectSummaryDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int ClassCount { get; set; }
        public bool HasSurvey { get; set; }
    }
}
