using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_m_educations")]
    public class Educations : BaseEntity
    {
        [Column("major", TypeName = "nvarchar(100)")] public string Major { get; set; }
        [Column("degree", TypeName = "nvarchar(100)")] public string Degree { get; set; }
        [Column("gpa")] public float Gpa { get; set; }
        [Column("university_guid")] public Guid UniversityGuid { get; set; }

        // Relationship Cardinality
        public University? University { get; set; }
        public Employees? Employees { get; set; }

    }
}
