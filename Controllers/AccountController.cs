using Booking_Api.Contracts;
using Booking_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // Get: api/Account
        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(accounts);
        }

        // Get: api/Account/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var account = _accountRepository.GetById(guid);
            if (account is null)
            {
                return NotFound("Account Not found");
            }
            return Ok(account);
        }

        // Post: api/Account
        [HttpPost]
        public IActionResult Create(Accounts account)
        {
            var createdAccount = _accountRepository.Create(account);
            if (createdAccount is null)
            {
                return BadRequest("Not Created Account. Try Again!");
            }
            return Ok(createdAccount);
        }

        // Put: api/Account
        [HttpPut]
        public IActionResult Update(Accounts account)
        {
            var updatedAccount = _accountRepository.Update(account);
            if (!updatedAccount)
            {
                return BadRequest("Not Updated Account. Try Again!");
            }
            return Ok(updatedAccount);
        }

        // Delete: api/Account
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var account = new Accounts() { Guid = guid };
            var deletedAccount = _accountRepository.Delete(account);
            if (!deletedAccount)
            {
                return BadRequest("Not Deleted Account. Try Again!");
            }
            return Ok(deletedAccount);
        }

    }
}
