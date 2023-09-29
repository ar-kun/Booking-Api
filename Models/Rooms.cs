using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_rooms")]
    public class Rooms : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")] public string Name { get; set; }
        [Column("floor")] public int Floor { get; set; }
        [Column("capacity")] public int Capacity { get; set; }

        // Relationship Cardinality (One to Many)
        public ICollection<Bookings>? Bookings { get; set; }

    }
}
