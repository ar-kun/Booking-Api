using Booking_Api.DTOs.Universities;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Universities
{
    public class CreateUniversityValidator : AbstractValidator<CreateUniversityDto>
    {
        public CreateUniversityValidator()
        {
            RuleFor(e => e.Code)
            .NotEmpty().WithMessage("Code is required")
            .MaximumLength(50).WithMessage("Code can't be longer than 50 characters");

            RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");
        }
    }
}
