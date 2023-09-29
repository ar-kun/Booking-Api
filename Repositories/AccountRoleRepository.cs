using Booking_Api.Contracts;
using Booking_Api.Data;
using Booking_Api.Models;

namespace Booking_Api.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingManagementDbContext _context;

        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public AccountRoles? Create(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Add(accountRole);
                _context.SaveChanges();
                return accountRole;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Remove(accountRole);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountRoles> GetAll()
        {
            return _context.Set<AccountRoles>().ToList();
        }

        public AccountRoles? GetById(Guid guid)
        {
            return _context.Set<AccountRoles>().Find(guid);
        }

        public bool Update(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Update(accountRole);
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
