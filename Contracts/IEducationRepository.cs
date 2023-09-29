using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IEducationRepository
    {
        IEnumerable<Educations> GetAll();
        Educations? GetById(Guid guid);
        Educations? Create(Educations education);
        bool Update(Educations education);
        bool Delete(Educations education);
    }
}
