using Booking_Api.DTOs.Educations;
using Booking_Api.DTOs.Employees;
using Booking_Api.DTOs.Universities;
using Booking_Api.Utilities.Enum;

namespace Booking_Api.DTOs.Accounts
{
    public class RegisterDto
    {
        public CreateEmployeeDto Employee { get; set; }
        public CreateAccountDto Account { get; set; }
        public CreateUniversityDto University { get; set; }
        public CreateEducationDto Education { get; set; }
    }
}
