using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BookingManagementDbContext _context;

        public AccountRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Accounts? Create(Accounts account)
        {
            try
            {
                _context.Set<Accounts>().Add(account);
                _context.SaveChanges();
                return account;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Accounts account)
        {
            try
            {
                _context.Set<Accounts>().Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Accounts> GetAll()
        {
            return _context.Set<Accounts>().ToList();
        }

        public Accounts? GetById(Guid guid)
        {
            return _context.Set<Accounts>().Find(guid);
        }

        public bool Update(Accounts account)
        {
            try
            {
                _context.Set<Accounts>().Update(account);
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
