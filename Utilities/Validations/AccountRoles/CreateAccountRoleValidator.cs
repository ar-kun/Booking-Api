using Booking_Api.DTOs.AccountRoles;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.AccountRoles
{
    public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
    {
        public CreateAccountRoleValidator()
        {
            RuleFor(e => e.AccountGuid)
            .NotEmpty().WithMessage("Account id is required");

            RuleFor(e => e.RoleGuid)
            .NotEmpty().WithMessage("Role id is required");
        }
    }
}
