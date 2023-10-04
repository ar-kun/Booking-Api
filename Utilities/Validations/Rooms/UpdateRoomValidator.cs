using Booking_Api.DTOs.Rooms;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.Rooms
{
    public class UpdateRoomValidator : AbstractValidator<RoomDto>
    {
        public UpdateRoomValidator()
        {
            RuleFor(e => e.Guid)
            .NotEmpty().WithMessage("Guid is required");

            RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name can't be longer than 100 characters");

            RuleFor(e => e.Floor)
            .NotEmpty().WithMessage("Floor is required")
            .GreaterThan(0).WithMessage("Floor must be greater than 0");

            RuleFor(e => e.Capacity)
            .NotEmpty().WithMessage("Capacity is required")
            .GreaterThan(0).WithMessage("Capacity must be greater than 0");
        }
    }
}
