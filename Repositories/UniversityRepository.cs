using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly BookingManagementDbContext _context;

        public UniversityRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public University? Create(University university)
        {
            try
            {
                _context.Set<University>().Add(university);
                _context.SaveChanges();
                return university;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(University university)
        {
            try
            {
                _context.Set<University>().Remove(university);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList();
        }

        public University? GetById(Guid guid)
        {
            return _context.Set<University>().Find(guid);
        }

        public bool Update(University university)
        {
            try
            {
                _context.Set<University>().Update(university);
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
