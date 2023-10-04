using Booking_Api.DTOs.Roles;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Roles
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {
            RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");
        }
    }
}
