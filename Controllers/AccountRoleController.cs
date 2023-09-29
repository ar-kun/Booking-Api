using Booking_Api.Contracts;
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

            return Ok(accountRoles);
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
            return Ok(accountRole);
        }

        // Post: api/AccountRole
        [HttpPost]
        public IActionResult Create(AccountRoles accountRole)
        {
            var createdAccountRole = _accountRoleRepository.Create(accountRole);
            if (createdAccountRole is null)
            {
                return BadRequest("Not Created AccountRole. Try Again!");
            }
            return Ok(createdAccountRole);
        }

        // Put: api/AccountRole
        [HttpPut]
        public IActionResult Update(AccountRoles accountRole)
        {
            var updatedAccountRole = _accountRoleRepository.Update(accountRole);
            if (!updatedAccountRole)
            {
                return BadRequest("Not Updated AccountRole. Try Again!");
            }
            return Ok(updatedAccountRole);
        }

        // Delete: api/AccountRole
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var result = new AccountRoles() { Guid = guid };
            var deletedAccountRole = _accountRoleRepository.Delete(result);
            if (!deletedAccountRole)
            {
                return BadRequest("Not Deleted AccountRole. Try Again!");
            }
            return Ok(deletedAccountRole);
        }
    }
}
