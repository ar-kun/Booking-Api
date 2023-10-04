using Booking_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_Api.Data
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) { }

        public DbSet<University> Universities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employe> Employees { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employe>().HasIndex(e => new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            // University - Educations Relationship (One to Many)
            modelBuilder.Entity<Education>()
                .HasOne(e => e.University)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UniversityGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Rooms - Bookings Relationship (One to Many)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Rooms)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Employees - Bookings Relationship (One to Many)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employees)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EmployeeGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Roles - AccountRoles Relationship (One to Many)
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Roles)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(ar => ar.RoleGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Accounts - AccountRoles Relationship (One to Many)
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Accounts)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(ar => ar.AccountGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Accounts - Employees Relationship (One to One)
            modelBuilder.Entity<Employe>()
                .HasOne(e => e.Accounts)
                .WithOne(a => a.Employees)
                .HasForeignKey<Account>(a => a.Guid)
                .OnDelete(DeleteBehavior.Cascade);

            // Employees - Educations Relationship (One to one)
            modelBuilder.Entity<Education>()
                .HasOne(e => e.Employees)
                .WithOne(e => e.Educations)
                .HasForeignKey<Education>(e => e.Guid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
