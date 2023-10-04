using Booking_Api.Models;

namespace Booking_Api.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static explicit operator AccountDto(Account account) // Operator explicit untuk mengkonversi Account menjadi AccountDto.
        {
            return new AccountDto // Mengembalikan object AccountDto dengan data dari property Account.
            {
                Guid = account.Guid,
                Password = account.Password,
                Otp = account.Otp,
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime
            };
        }

        public static implicit operator Account(AccountDto accountDto) // Operator implicit untuk mengkonversi AccountDto menjadi Account.
        {
            return new Account // Mengembalikan object Account dengan data dari property AccountDto.
            {
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                Otp = accountDto.Otp,
                IsUsed = accountDto.IsUsed,
                ExpiredTime = accountDto.ExpiredTime,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
