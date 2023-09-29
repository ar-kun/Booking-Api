using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Accounts> GetAll();
        Accounts? GetById(Guid guid);
        Accounts? Create(Accounts account);
        bool Update(Accounts account);
        bool Delete(Accounts account);
    }
}
