using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class EducationRepository : GeneralRepository<Education>, IEducationRepository
    {
        public EducationRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
