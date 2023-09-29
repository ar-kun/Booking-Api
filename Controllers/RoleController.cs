using Booking_Api.Contracts;
using Booking_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // Get: api/Role
        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(roles);
        }

        // Get: api/Role/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var role = _roleRepository.GetById(guid);
            if (role is null)
            {
                return NotFound("Role Not found");
            }
            return Ok(role);
        }

        // Post: api/Role
        [HttpPost]
        public IActionResult Create(Roles role)
        {
            var createdRole = _roleRepository.Create(role);
            if (createdRole is null)
            {
                return BadRequest("Not Created Role. Try Again!");
            }
            return Ok(createdRole);
        }

        // Put: api/Role
        [HttpPut]
        public IActionResult Update(Roles role)
        {
            var updatedRole = _roleRepository.Update(role);
            if (!updatedRole)
            {
                return BadRequest("Not Updated Role. Try Again!");
            }
            return Ok(updatedRole);
        }

        // Delete: api/Role
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var role = new Roles() { Guid = guid };
            var deletedRole = _roleRepository.Delete(role);
            if (!deletedRole)
            {
                return BadRequest("Not Deleted Role. Try Again!");
            }
            return Ok(deletedRole);
        }
    }
}
