using Booking_Api.DTOs.Bookings;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Bookings
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidator()
        {
            RuleFor(e => e.StartDate)
            .NotEmpty().WithMessage("Start date is required")
            .Must(BeValidStartDate).WithMessage("Start date must be today or later");

            RuleFor(e => e.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .Must((model, endDate) => endDate > model.StartDate).WithMessage("End date must be greater than Start date");

            RuleFor(e => e.Status)
            .NotEmpty().WithMessage("Status is required")
            .IsInEnum().WithMessage("Status must be a valid BookingStatus");

            RuleFor(e => e.Remarks)
            .NotEmpty().WithMessage("Remarks is required")
            .MaximumLength(100).WithMessage("Remarks can't be longer than 100 characters");

            RuleFor(e => e.RoomGuid)
            .NotEmpty().WithMessage("Room id is required");

            RuleFor(e => e.EmployeeGuid)
            .NotEmpty().WithMessage("Employee id is required");
        }

        private bool BeValidStartDate(DateTime startDate)
        {
            return startDate.Date >= DateTime.Now.Date;
        }
    }
}
