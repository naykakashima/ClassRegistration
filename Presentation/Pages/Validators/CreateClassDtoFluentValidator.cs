using FluentValidation;
using ClassRegistrationApplication2025.Application.DTOs;
using System.Runtime.CompilerServices;

namespace ClassRegistrationApplication2025.Presentation.Pages.Validators
{
    public class CreateClassDtoFluentValidator : AbstractValidator<CreateClassDto>
    {
        public CreateClassDtoFluentValidator()
        {
            RuleFor(x => x.ClassName)
                .MaximumLength(100);

            RuleFor(x => x.SessionName)
                .MaximumLength(100);

            RuleFor(x => x.PresenterName)
                .MaximumLength(100);

            RuleFor(x => x.Date)
                .NotNull()
                .WithMessage("Date is required.")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Date must be today or in the future.");

            RuleFor(x => x.StartTime)
                .NotNull()
                .WithMessage("Start time is required.")
                .LessThan(x => x.EndTime)
                .When(x => x.EndTime.HasValue)
                .WithMessage("Start time must be before end time.");

            RuleFor(x => x.EndTime)
                .NotNull()
                .WithMessage("End time is required.")
                .GreaterThan(x => x.StartTime)
                .When(x => x.StartTime.HasValue)
                .WithMessage("End time must be after start time.");

            RuleFor(x => x.MaxSlots)
                .GreaterThan(0)
                .LessThan(999);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var instance = (CreateClassDto)model;

                var context = ValidationContext<CreateClassDto>.CreateWithOptions(instance, x => x.IncludeProperties(propertyName));
                var result = await ValidateAsync(context);
                return result.IsValid
                    ? Array.Empty<string>()
                    : result.Errors.Select(e => e.ErrorMessage);
            };

        public Func<object, string, Task<IEnumerable<string>>> ConditionalValidateValue(Func<string, bool> shouldValidate) => async (model, propertyName) =>
            {
                if (!shouldValidate(propertyName))
                    return Array.Empty<string>();

                var context = ValidationContext<CreateClassDto>.CreateWithOptions((CreateClassDto)model, x => x.IncludeProperties(propertyName));
                var result = await ValidateAsync(context);

                return result.IsValid
                    ? Array.Empty<string>()
                    : result.Errors.Select(e => e.ErrorMessage);

                
            };

          


    }
}
