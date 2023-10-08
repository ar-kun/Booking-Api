using System.Security.Claims;

namespace Booking_Api.Contracts
{
    public interface ITokenHandler
    {
        string Generate(IEnumerable<Claim> claims);
    }
}
