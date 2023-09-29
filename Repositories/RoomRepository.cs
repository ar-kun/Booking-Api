using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingManagementDbContext _context;

        public RoomRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Rooms? Create(Rooms rooms)
        {
            try
            {
                _context.Set<Rooms>().Add(rooms);
                _context.SaveChanges();
                return rooms;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Rooms rooms)
        {
            try
            {
                _context.Set<Rooms>().Remove(rooms);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Rooms> GetAll()
        {
            return _context.Set<Rooms>().ToList();
        }

        public Rooms? GetById(Guid guid)
        {
            return _context.Set<Rooms>().Find(guid);
        }

        public bool Update(Rooms rooms)
        {
            try
            {
                _context.Set<Rooms>().Update(rooms);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
