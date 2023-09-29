using Booking_Api.Models;

namespace Booking_Api.Contracts
{
    public interface IRoomRepository
    {
        IEnumerable<Rooms> GetAll();
        Rooms? GetById(Guid guid);
        Rooms? Create(Rooms rooms);
        bool Update(Rooms rooms);
        bool Delete(Rooms rooms);
    }
}
