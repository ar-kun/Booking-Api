using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IRoleRepository
    {
        IEnumerable<Roles> GetAll();
        Roles? GetById(Guid guid);
        Roles? Create(Roles roles);
        bool Update(Roles roles);
        bool Delete(Roles roles);
    }
}
