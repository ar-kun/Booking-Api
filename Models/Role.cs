using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")] public string Name { get; set; }

        // Relationship Cardinality (One to Many)
        public ICollection<AccountRole>? AccountRoles { get; set; }

    }
}
