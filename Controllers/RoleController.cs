using Booking_Api.Contracts;
using Booking_Api.DTOs.Roles;
using Booking_Api.DTOs.Rooms;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = roles.Select(x => (RoleDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<RoleDto>>(data));
        }

        // Get: api/Role/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var role = _roleRepository.GetById(guid);
            if (role is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<RoleDto>((RoleDto)role));
        }

        // Post: api/Role
        [HttpPost]
        public IActionResult Create(CreateRoleDto createRoleDto)
        {
            try
            {
                var createdRole = _roleRepository.Create(createRoleDto);
                
                return Ok(new ResponseOKHandler<RoleDto>((RoleDto)createdRole));
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

        // Put: api/Role
        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            try
            {
                var role = _roleRepository.GetById(roleDto.Guid);
                if (role is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                Role toUpdate = roleDto;
                toUpdate.CreatedDate = role.CreatedDate;

                _roleRepository.Update(role);
                
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

        // Delete: api/Role
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var role = _roleRepository.GetById(guid);
                if (role is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                 _roleRepository.Delete(role);
                
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
