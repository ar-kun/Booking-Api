namespace Booking_Api.Utilities.Validations.Rooms
{
    public class UsedRoomDto
    {
        public Guid BookingGuid { get; set; } 
        public string RoomName { get; set; } 
        public string Status { get; set; } 
        public int Floor { get; set; } 
        public string BookBy { get; set; } 
    }
}
