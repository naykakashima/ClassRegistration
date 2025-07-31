namespace ClassRegistrationApplication2025.Domain.Entities
{
    public class ClassSurvey : SurveyBase
    {
        public Guid? ClassId { get; set; }
        public Class Class { get; set; }
    }
}
