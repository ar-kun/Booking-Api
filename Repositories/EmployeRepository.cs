using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class EmployeRepository : GeneralRepository<Employe>, IEmployeRepository
    {
        public EmployeRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
