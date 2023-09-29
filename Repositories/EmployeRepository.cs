using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class EmployeRepository : IEmployeRepository
    {
        private readonly BookingManagementDbContext _context;

        public EmployeRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Employees? Create(Employees employe)
        {
            try
            {
                _context.Set<Employees>().Add(employe);
                _context.SaveChanges();
                return employe;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Employees employe)
        {
            try
            {
                _context.Set<Employees>().Remove(employe);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Employees> GetAll()
        {
            return _context.Set<Employees>().ToList();
        }

        public Employees? GetById(Guid guid)
        {
            return _context.Set<Employees>().Find(guid);
        }

        public bool Update(Employees employe)
        {
            try
            {
                _context.Set<Employees>().Update(employe);
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
