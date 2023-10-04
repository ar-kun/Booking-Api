using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {
        [Column("account_guid")] public Guid AccountGuid { get; set; }
        [Column("role_guid")] public Guid RoleGuid { get; set; }

        // Relationship Cardinality 
        public Role? Roles { get; set; }
        public Account? Accounts { get; set; }
    }
}
