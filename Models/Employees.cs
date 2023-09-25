﻿namespace Booking_Api.Models
{
    public class Employees
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
