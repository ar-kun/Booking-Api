namespace Booking_Api.DTOs.Employees
{
    public class ForgotePasswordDto
    {
        public int Otp { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
