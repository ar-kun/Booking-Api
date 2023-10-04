using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
