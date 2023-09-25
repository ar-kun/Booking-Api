using Booking_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_Api.Data
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) { }

        public DbSet<University> Universities { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<AccountRoles> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>().HasIndex(e => new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();
        }
    }
}
