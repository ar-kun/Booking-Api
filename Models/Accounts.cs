namespace Booking_Api.Models
{
    public class Accounts
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int Otp {  get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set;}
    }
}
