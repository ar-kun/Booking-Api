using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly BookingManagementDbContext _context;

        public EducationRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Educations? Create(Educations education)
        {
            try
            {
                _context.Set<Educations>().Add(education);
                _context.SaveChanges();
                return education;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Educations education)
        {
            try
            {
                _context.Set<Educations>().Remove(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Educations> GetAll()
        {
            return _context.Set<Educations>().ToList();
        }

        public Educations? GetById(Guid guid)
        {
            return _context.Set<Educations>().Find(guid);
        }

        public bool Update(Educations education)
        {
            try
            {
                _context.Set<Educations>().Update(education);
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
