using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_universities")]
    public class University : BaseEntity
    {
        [Column("code", TypeName = "nvarchar(50)")] public string Code { get; set; }
        [Column("name", TypeName = "nvarchar(100)")] public string Name { get; set; }
    }
}
