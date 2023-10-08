namespace Booking_Api.DTOs.Bookings
{
    public class TimeBookingDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public int BookingTime { get; set; }
    }
}
