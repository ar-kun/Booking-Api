using System.Data;
using Booking_Api.DTOs.Accounts;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Accounts
{
    public class UpdateAccountValidator : AbstractValidator<AccountDto>
    {
        public UpdateAccountValidator()
        {
            RuleFor(e => e.Guid)
            .NotEmpty().WithMessage("Guid is required");

            RuleFor(e => e.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(225).WithMessage("Password can't be longer than 225 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 special character");

            RuleFor(e => e.Otp)
            .NotEmpty().WithMessage("OTP is required")
            .Must(BeValidOtp).WithMessage("OTP must be 6 digits");

            RuleFor(e => e.ExpiredTime)
            .NotEmpty().WithMessage("Expired time is required")
            .Must(BeValidStartDate).WithMessage("Expired time must be greater than or equal to current time");
        }

        private bool BeValidOtp(int otp)
        {
            return otp.ToString().Length == 6;
        }

        private bool BeValidStartDate(DateTime startDate)
        {
            return startDate.Date >= DateTime.Now.Date;
        }
    }
}
