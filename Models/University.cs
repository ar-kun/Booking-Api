namespace Booking_Api.Models
{
    public class University
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
