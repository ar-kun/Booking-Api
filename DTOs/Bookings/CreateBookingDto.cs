using Booking_Api.Models;
using Booking_Api.Utilities.Enum;

namespace Booking_Api.DTOs.Bookings
{
    public class CreateBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLavel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        public static implicit operator Booking(CreateBookingDto createBookingDto)
        {
            return new Booking
            {
                StartDate = createBookingDto.StartDate,
                EndDate = createBookingDto.EndDate,
                Status = (int)createBookingDto.Status,
                Remarks = createBookingDto.Remarks,
                RoomGuid = createBookingDto.RoomGuid,
                EmployeeGuid = createBookingDto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
