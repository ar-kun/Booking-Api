﻿using Booking_Api.Models;

namespace Booking_Api.DTOs.Accounts
{
    public class CreateAccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static implicit operator Account(CreateAccountDto createAccountDto) // Operator implicit untuk mengkonversi CreateAccountDto menjadi Account.
        {
            return new Account // Mengembalikan object Account dengan data dari property CreateAccountDto.
            {
                Guid = createAccountDto.Guid,
                Password = createAccountDto.Password,
                Otp = createAccountDto.Otp,
                IsUsed = createAccountDto.IsUsed,
                ExpiredTime = createAccountDto.ExpiredTime,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}