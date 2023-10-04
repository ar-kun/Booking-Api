using Booking_Api.Contracts;
using Booking_Api.DTOs.Roles;
using Booking_Api.DTOs.Rooms;
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

            var data = roles.Select(x => (RoleDto)x);

            return Ok(data);
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
            return Ok((RoleDto)role);
        }

        // Post: api/Role
        [HttpPost]
        public IActionResult Create(CreateRoleDto createRoleDto)
        {
            var createdRole = _roleRepository.Create(createRoleDto);
            if (createdRole is null)
            {
                return BadRequest("Not Created Role. Try Again!");
            }
            return Ok((RoleDto)createdRole);
        }

        // Put: api/Role
        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            var role = _roleRepository.GetById(roleDto.Guid);
            if (role is null)
            {
                return NotFound("Id Not Found");
            }
            Role toUpdate = roleDto;
            toUpdate.CreatedDate = role.CreatedDate;

            var updatedRole = _roleRepository.Update(role);
            if (!updatedRole)
            {
                return BadRequest("Not Updated Role. Try Again!");
            }
            return Ok("Data has been updated successfully");
        }

        // Delete: api/Role
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var role = _roleRepository.GetById(guid);
            if (role is null)
            {
                return NotFound("Id Not Found");
            }
            var deletedRole = _roleRepository.Delete(role);
            if (!deletedRole)
            {
                return BadRequest("Not Deleted Role. Try Again!");
            }
            return Ok(deletedRole);
        }
    }
}
