using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingManagementDbContext _context;

        public BookingRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Bookings? Create(Bookings booking)
        {
            try
            {
                _context.Set<Bookings>().Add(booking);
                _context.SaveChanges();
                return booking;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Bookings booking)
        {
            try
            {
                _context.Set<Bookings>().Remove(booking);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Bookings> GetAll()
        {
            return _context.Set<Bookings>().ToList();
        }

        public Bookings? GetById(Guid guid)
        {
            return _context.Set<Bookings>().Find(guid);
        }

        public bool Update(Bookings booking)
        {
            try
            {
                _context.Set<Bookings>().Update(booking);
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
