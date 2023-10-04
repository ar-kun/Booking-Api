using Booking_Api.Contracts;
using Booking_Api.DTOs.AccountRoles;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        // Get: api/AccountRole
        [HttpGet]
        public IActionResult GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            var data = accountRoles.Select(x => (AccountRoleDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<AccountRoleDto>>(data));
        }

        // Get: api/AccountRole/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetById(guid);
            if (accountRole is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<AccountRoleDto>((AccountRoleDto)accountRole));
        }

        // Post: api/AccountRole
        [HttpPost]
        public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
        {
            try
            {
                var createdAccountRole = _accountRoleRepository.Create(createAccountRoleDto);
                
                return Ok(new ResponseOKHandler<AccountRoleDto>((AccountRoleDto)createdAccountRole));
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

        // Put: api/AccountRole
        [HttpPut]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            try
            {
                var accountRole = _accountRoleRepository.GetById(accountRoleDto.Guid);
                if (accountRole is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                AccountRole toUpdate = accountRoleDto;
                toUpdate.CreatedDate = accountRole.CreatedDate;

                _accountRoleRepository.Update(accountRole);
                
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

        // Delete: api/AccountRole
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var accountRole = _accountRoleRepository.GetById(guid);
                if (accountRole is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                _accountRoleRepository.Delete(accountRole);
                
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
