using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_accounts")]
    public class Accounts : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(225)")] public string Password { get; set; }
        [Column("is_deleted")] public bool IsDeleted { get; set; }
        [Column("otp")] public int Otp { get; set; }
        [Column("is_used")] public bool IsUsed { get; set; }
        [Column("exoired_time")] public DateTime ExpiredTime { get; set; }

        // Relationship Cardinality (One to Many)
        public ICollection<AccountRoles>? AccountRoles { get; set; }
        public Employees? Employees { get; set; }
    }
}
