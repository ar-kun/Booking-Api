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

            // University - Educations Relationship (One to Many)
            modelBuilder.Entity<Educations>()
                .HasOne(e => e.University)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UniversityGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Rooms - Bookings Relationship (One to Many)
            modelBuilder.Entity<Bookings>()
                .HasOne(b => b.Rooms)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Employees - Bookings Relationship (One to Many)
            modelBuilder.Entity<Bookings>()
                .HasOne(b => b.Employees)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EmployeeGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Roles - AccountRoles Relationship (One to Many)
            modelBuilder.Entity<AccountRoles>()
                .HasOne(ar => ar.Roles)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(ar => ar.RoleGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Accounts - AccountRoles Relationship (One to Many)
            modelBuilder.Entity<AccountRoles>()
                .HasOne(ar => ar.Accounts)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(ar => ar.AccountGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // Accounts - Employees Relationship (One to One)
            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Accounts)
                .WithOne(a => a.Employees)
                .HasForeignKey<Accounts>(a => a.Guid)
                .OnDelete(DeleteBehavior.Cascade);

            // Employees - Educations Relationship (One to one)
            modelBuilder.Entity<Educations>()
                .HasOne(e => e.Employees)
                .WithOne(e => e.Educations)
                .HasForeignKey<Educations>(e => e.Guid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
