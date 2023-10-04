using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_Api.Repositories
{
    public class EmployeRepository : GeneralRepository<Employe>, IEmployeRepository
    {
        public EmployeRepository(BookingManagementDbContext context) : base(context)
        {
        }
        public string? GetLastNik()
        {
            return _context.Set<Employe>().OrderBy(e => e.Nik).LastOrDefault()?.Nik;
        }
    }
}
