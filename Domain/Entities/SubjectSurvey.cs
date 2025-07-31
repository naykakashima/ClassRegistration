namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class SubjectSurvey : SurveyBase
    {
        public Guid? SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
