using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IAccountRoleRepository
    {
        IEnumerable<AccountRoles> GetAll();
        AccountRoles? GetById(Guid guid);
        AccountRoles? Create(AccountRoles accountRole);
        bool Update(AccountRoles accountRole);
        bool Delete(AccountRoles accountRole);
    }
}
