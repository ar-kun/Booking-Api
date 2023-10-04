using Booking_Api.DTOs.Educations;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Educations
{
    public class UpdateEducationValidator : AbstractValidator<EducationDto>
    {
        public UpdateEducationValidator()
        {
            RuleFor(e => e.Guid)
            .NotEmpty().WithMessage("Guid is required");

            RuleFor(e => e.Major)
            .NotEmpty().WithMessage("Major is required")
            .MaximumLength(100).WithMessage("Major can't be longer than 100 characters");

            RuleFor(e => e.Degree)
            .NotEmpty().WithMessage("Degree is required")
            .MaximumLength(100).WithMessage("Degree can't be longer than 100 characters");

            RuleFor(e => e.Gpa)
            .NotEmpty().WithMessage("GPA is required")
            .GreaterThanOrEqualTo(0).WithMessage("GPA must be greater than or equal to 0")
            .LessThanOrEqualTo(4).WithMessage("GPA must be less than or equal to 4");

            RuleFor(e => e.UniversityGuid)
            .NotEmpty().WithMessage("University id is required");
        }
    }
}
