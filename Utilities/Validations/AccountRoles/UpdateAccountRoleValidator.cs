using System.Data;
using Booking_Api.DTOs.AccountRoles;
using FluentValidation;

namespace Booking_Api.Utilities.Validations.AccountRoles
{
    public class UpdateAccountRoleValidator : AbstractValidator<AccountRoleDto>
    {
        public UpdateAccountRoleValidator()
        {
            RuleFor(e => e.Guid)
            .NotEmpty().WithMessage("Guid is required");

            RuleFor(e => e.AccountGuid)
            .NotEmpty().WithMessage("Account id is required");

            RuleFor(e => e.RoleGuid)
            .NotEmpty().WithMessage("Role id is required");
        }
    }
}
