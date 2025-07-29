using ClassRegistrationApplication2025.Application.DTOs;
using FluentValidation;

namespace ClassRegistrationApplication2025.Presentation.Pages.Validators
{
    public class CreateSubjectDtoFluentValidator : AbstractValidator<CreateSubjectDto>
    {
        public CreateSubjectDtoFluentValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be more than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(300).WithMessage("Description cannot exceed 300 characters.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var context = ValidationContext<CreateSubjectDto>.CreateWithOptions((CreateSubjectDto)model, x => x.IncludeProperties(propertyName));
            var result = await ValidateAsync(context);
            return result.IsValid
                ? Array.Empty<string>()
                : result.Errors.Select(e => e.ErrorMessage);
        };

        public Func<object, string, Task<IEnumerable<string>>> ConditionalValidateValue(Func<string, bool> shouldValidate) => async (model, propertyName) =>
        {
            if (!shouldValidate(propertyName))
                return Array.Empty<string>();

            var context = ValidationContext<CreateSubjectDto>.CreateWithOptions((CreateSubjectDto)model, x => x.IncludeProperties(propertyName));
            var result = await ValidateAsync(context);
            return result.IsValid
                ? Array.Empty<string>()
                : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
