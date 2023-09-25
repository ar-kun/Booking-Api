using System.ComponentModel.DataAnnotations.Schema;
using BookingApp.Models;

namespace Booking_Api.Models
{
    [Table(name: "tb_tr_bookings")]
    public class Bookings : BaseEntity
    {
        [Column("start_date")] public DateTime StartDate { get; set; }
        [Column("end_date")] public DateTime EndDate { get; set; }
        [Column("status")] public int Status { get; set; }
        [Column("remarks", TypeName = "nvarchar(100)")] public string Remarks { get; set; }
        [Column("room_guid")] public Guid RoomGuid { get; set; }
        [Column("employee_guid")] public Guid EmployeeGuid { get; set; }

    }
}
