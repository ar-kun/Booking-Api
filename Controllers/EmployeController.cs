using Booking_Api.Contracts;
using Booking_Api.DTOs.Employees;
using Booking_Api.Models;
using Booking_Api.Repositories;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeController : ControllerBase
    {
        private readonly IEmployeRepository _employeRepository;

        public EmployeController(IEmployeRepository employeRepository)
        {
            _employeRepository = employeRepository;
        }

        // Get: api/Employe
        [HttpGet]
        public IActionResult GetAll()
        {
            var employes = _employeRepository.GetAll();
            if (!employes.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = employes.Select(x => (EmployeeDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
        }

        // Get: api/Employe/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var employe = _employeRepository.GetById(guid);
            if (employe is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)employe));
        }

        // Post: api/Employe
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            try {
                var createdEmploye = _employeRepository.Create(createEmployeeDto);
                return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)createdEmploye));
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

        // Put: api/Employe
        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try
            {
                var employe = _employeRepository.GetById(employeeDto.Guid);
                if (employe is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                Employe toUpdate = employeeDto;
                toUpdate.CreatedDate = employe.CreatedDate;
                _employeRepository.Update(employe);
                return Ok(new ResponseOKHandler<string>("Data has been updated successfully"));
            }
            catch (ExceptionHandler ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        // Delete: api/Employe
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var employe = _employeRepository.GetById(guid);
                if (employe is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                _employeRepository.Delete(employe);
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
