using Booking_Api.Models;

namespace Booking_Api.DTOs.Accounts
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public int Otp { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }


        public static implicit operator Account(ChangePasswordDto changePasswordDto)
        {
            return new Account
            {
                Password = changePasswordDto.NewPassword,
                Otp = changePasswordDto.Otp,
                IsUsed = true,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
