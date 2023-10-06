namespace Booking_Api.Utilities.Handler
{
    public class GenerateHandler
    {
        // public static string Nik(string? lastNik = null)
        // {
        //     if (lastNik is null)
        //     {
        //         return "111111"; // First employee
        //     }

        //     var generateNik = Convert.ToInt32(lastNik) + 1;

        //     return generateNik.ToString();
        // }

        public int Otp()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            return randomNumber;
        }

        public DateTime ExpireTime()
        {
            DateTime now = DateTime.Now;
            DateTime expireTime = now.AddMinutes(5);
            return expireTime;
        }

        public int GenerateRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            return randomNumber;
        }
    }
}
