using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Booking_Api.Utilities.Enum;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_employees")]
    public class Employees : BaseEntity
    {
        [Key, Column("nik", TypeName = "nchar(6)")] public string Nik { get; set; }
        [Column("first_name", TypeName = "nvarchar(100)")] public string FirstName { get; set; }
        [Column("last_name", TypeName = "nvarchar(100)")] public string? LastName { get; set; }
        [Column("birth_date")] public DateTime BirthDate { get; set; }
        [Column("gender")] public GenderLavel Gender { get; set; }
        [Column("hiring_date")] public DateTime HiringDate { get; set; }
        [Column("email", TypeName = "nvarchar(100)")] public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar(100)")] public string PhoneNumber { get; set; }

    }

}
