namespace ClassRegistrationApplication2025.Application.DTOs
{
    public interface ISurveyBaseDto
    {
        Guid Id { get; }
        string Title { get; }
        string? Description { get; }
        DateTime CreatedAt { get; }
    }
}
