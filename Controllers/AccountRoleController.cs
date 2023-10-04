using Booking_Api.Contracts;
using Booking_Api.DTOs.AccountRoles;
using Booking_Api.Models;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound("Data Not Found");
            }
            var data = accountRoles.Select(x => (AccountRoleDto)x);

            return Ok(data);
        }

        // Get: api/AccountRole/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetById(guid);
            if (accountRole is null)
            {
                return NotFound("AccountRole Not found");
            }
            return Ok((AccountRoleDto)accountRole);
        }

        // Post: api/AccountRole
        [HttpPost]
        public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
        {
            var createdAccountRole = _accountRoleRepository.Create(createAccountRoleDto);
            if (createdAccountRole is null)
            {
                return BadRequest("Not Created AccountRole. Try Again!");
            }
            return Ok((AccountRoleDto)createdAccountRole);
        }

        // Put: api/AccountRole
        [HttpPut]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            var accountRole = _accountRoleRepository.GetById(accountRoleDto.Guid);
            if (accountRole is null)
            {
                return NotFound("Id Not Found");
            }
            AccountRole toUpdate = accountRoleDto;
            toUpdate.CreatedDate = accountRole.CreatedDate;

            var updatedAccountRole = _accountRoleRepository.Update(accountRole);
            if (!updatedAccountRole)
            {
                return BadRequest("Not Updated AccountRole. Try Again!");
            }
            return Ok("Data has been updated successfully");
        }

        // Delete: api/AccountRole
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetById(guid);
            if (accountRole is null)
            {
                return NotFound("Id Not Found");
            }
            var deletedAccountRole = _accountRoleRepository.Delete(accountRole);
            if (!deletedAccountRole)
            {
                return BadRequest("Not Deleted AccountRole. Try Again!");
            }
            return Ok(deletedAccountRole);
        }
    }
}
