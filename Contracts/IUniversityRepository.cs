using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IUniversityRepository
    {
        IEnumerable<University> GetAll();
        University? GetById(Guid guid);
        University? Create(University university);
        bool Update(University university);
        bool Delete(University university);
    }
}
