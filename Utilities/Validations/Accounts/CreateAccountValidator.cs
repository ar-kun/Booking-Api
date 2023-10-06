using Booking_Api.DTOs.Accounts;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Accounts
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            RuleFor(e => e.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(225).WithMessage("Password can't be longer than 225 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 special character");

        }
    }
}
