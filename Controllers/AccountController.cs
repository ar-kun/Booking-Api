using Booking_Api.Contracts;
using Booking_Api.DTOs.Accounts;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
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

            var data = accounts.Select(x => (AccountDto)x);

            return Ok(data);
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
            return Ok((AccountDto)account);
        }

        // Post: api/Account
        [HttpPost]
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            Account toCreate = createAccountDto;
            toCreate.Password = HashingHandler.HashPassword(createAccountDto.Password);

            var createdAccount = _accountRepository.Create(createAccountDto);
            if (createdAccount is null)
            {
                return BadRequest("Not Created Account. Try Again!");
            }
            return Ok((AccountDto)createdAccount);
        }

        // Put: api/Account
        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            var account = _accountRepository.GetById(accountDto.Guid);
            if (account is null)
            {
                return NotFound("Id Not Found");
            }
            Account toUpdate = accountDto;
            toUpdate.CreatedDate = account.CreatedDate;
            toUpdate.Password = HashingHandler.HashPassword(accountDto.Password);
            var updatedAccount = _accountRepository.Update(account);
            if (!updatedAccount)
            {
                return BadRequest("Not Updated Account. Try Again!");
            }
            return Ok("Data has been updated successfully");
        }

        // Delete: api/Account
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var account = _accountRepository.GetById(guid);
            if (account is null)
            {
                return NotFound("Id Not Found");
            }
            var deletedAccount = _accountRepository.Delete(account);
            if (!deletedAccount)
            {
                return BadRequest("Not Deleted Account. Try Again!");
            }
            return Ok(deletedAccount);
        }

    }
}
