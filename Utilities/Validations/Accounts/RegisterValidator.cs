using System.Text.RegularExpressions;
using Booking_Api.DTOs.Accounts;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(e => e.Account.Password)
            .NotEmpty().WithMessage("Email is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(225).WithMessage("Password can't be longer than 225 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 special character");

            RuleFor(e => e.Employee.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MaximumLength(100).WithMessage("Last Name can't be longer than 100 characters");

            RuleFor(e => e.Employee.LastName)
            .MaximumLength(225).WithMessage("Last name can't be longer than 225 characters");

            RuleFor(e => e.Employee.Gender)
            .NotNull().WithMessage("Genre is required")
            .IsInEnum().WithMessage("Gender must be Male or Female");

            RuleFor(e => e.Employee.HiringDate)
            .NotEmpty().WithMessage("Hiring Date is required");

            RuleFor(e => e.Employee.Email)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(100).WithMessage("Email can't be longer than 100 characters")
            .EmailAddress().WithMessage("Email must be a valid email address");

            RuleFor(e => e.Employee.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .MinimumLength(10).WithMessage("Phone Number must be at least 10 characters")
            .MaximumLength(20).WithMessage("Phone Number can't be longer than 20 characters")
            .Must(BeValidIndonesianPhoneNumber).WithMessage("Phone Number must be a valid Indonesian phone number");

            RuleFor(e => e.Education.Major)
            .NotEmpty().WithMessage("Major is required")
            .MaximumLength(100).WithMessage("Major can't be longer than 100 characters");

            RuleFor(e => e.Education.Degree)
            .NotEmpty().WithMessage("Degree is required")
            .MaximumLength(100).WithMessage("Degree can't be longer than 100 characters");

            RuleFor(e => e.Education.Gpa)
            .NotEmpty().WithMessage("GPA is required")
            .GreaterThanOrEqualTo(0).WithMessage("GPA must be greater than or equal to 0")
            .LessThanOrEqualTo(4).WithMessage("GPA must be less than or equal to 4");

            RuleFor(e => e.University.Code)
            .NotEmpty().WithMessage("Code is required")
            .MaximumLength(50).WithMessage("Code can't be longer than 50 characters");

            RuleFor(e => e.University.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");
        }

        private bool BeValidIndonesianPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^(?:\+62|0)[0-9]{9,13}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}
