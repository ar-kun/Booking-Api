using System.Data;
using System.Text.RegularExpressions;
using Booking_Api.DTOs.Employees;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Employees
{
    public class CreateEmployeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeValidator()
        {
            RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MaximumLength(100).WithMessage("First Name can't be longer than 100 characters");

            RuleFor(e => e.LastName)
            .MaximumLength(100).WithMessage("Last Name can't be longer than 100 characters");

            RuleFor(e => e.BirthDate)
            .NotEmpty().WithMessage("Birth Date is required")
            .GreaterThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Birth Date must be greater than 18 years old");

            RuleFor(e => e.Gender)
            .NotEmpty().WithMessage("Genre is required")
            .IsInEnum().WithMessage("Gender must be Male or Female");

            RuleFor(e => e.HiringDate)
            .NotEmpty().WithMessage("Hiring Date is required");

            RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(100).WithMessage("Email can't be longer than 100 characters")
            .EmailAddress().WithMessage("Email must be a valid email address");

            RuleFor(e => e.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .MinimumLength(10).WithMessage("Phone Number must be at least 10 characters")
            .MaximumLength(20).WithMessage("Phone Number can't be longer than 20 characters")
            .Must(BeValidIndonesianPhoneNumber).WithMessage("Phone Number must be a valid Indonesian phone number");
        }
        private bool BeValidIndonesianPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^(?:\+62|0)[0-9]{9,13}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}
