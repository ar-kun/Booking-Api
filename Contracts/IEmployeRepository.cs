using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IEmployeRepository
    {
        IEnumerable<Employees> GetAll();
        Employees? GetById(Guid guid);
        Employees? Create(Employees employe);
        bool Update(Employees employe);
        bool Delete(Employees employe);
    }
}
