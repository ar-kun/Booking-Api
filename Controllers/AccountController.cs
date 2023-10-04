using Booking_Api.Contracts;
using Booking_Api.DTOs.Accounts;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = accounts.Select(x => (AccountDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
        }

        // Get: api/Account/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var account = _accountRepository.GetById(guid);
            if (account is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)account));
        }

        // Post: api/Account
        [HttpPost]
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            try
            {
                Account toCreate = createAccountDto;
                toCreate.Password = HashingHandler.HashPassword(createAccountDto.Password);

                var createdAccount = _accountRepository.Create(createAccountDto);
                
                return Ok(new ResponseOKHandler<AccountDto>((AccountDto)createdAccount));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        // Put: api/Account
        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                var account = _accountRepository.GetById(accountDto.Guid);
                if (account is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                Account toUpdate = accountDto;
                toUpdate.CreatedDate = account.CreatedDate;
                toUpdate.Password = HashingHandler.HashPassword(accountDto.Password);
                _accountRepository.Update(account);
                
                return Ok(new ResponseOKHandler<string>("Data has been updated successfully"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        // Delete: api/Account
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var account = _accountRepository.GetById(guid);
                if (account is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                _accountRepository.Delete(account);
                
                return Ok(new ResponseOKHandler<string>("Data has been Deleted successfully"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

    }
}
