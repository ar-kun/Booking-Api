using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IBookingRepository
    {
        IEnumerable<Bookings> GetAll();
        Bookings? GetById(Guid guid);
        Bookings? Create(Bookings booking);
        bool Update(Bookings booking);
        bool Delete(Bookings booking);
    }
}
