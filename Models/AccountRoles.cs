using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_account_roles")]
    public class AccountRoles : BaseEntity
    {
        [Column("account_guid")] public Guid AccountGuid { get; set; }
        [Column("role_guid")] public Guid RoleGuid { get; set; }

        // Relationship Cardinality 
        public Roles? Roles { get; set; }
        public Accounts? Accounts { get; set; }
    }
}
