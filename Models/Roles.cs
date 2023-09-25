using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_roles")]
    public class Roles : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")] public string Name { get; set; }

    }
}
