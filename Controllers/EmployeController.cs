using Booking_Api.Contracts;
using Booking_Api.DTOs.Employees;
using Booking_Api.Models;
using Booking_Api.Repositories;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound("Data Not Found");
            }

            var data = employes.Select(x => (EmployeeDto)x);

            return Ok(data);
        }

        // Get: api/Employe/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var employe = _employeRepository.GetById(guid);
            if (employe is null)
            {
                return NotFound("Employe Not found");
            }
            return Ok((EmployeeDto)employe);
        }

        // Post: api/Employe
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            var createdEmploye = _employeRepository.Create(createEmployeeDto);
            if (createdEmploye is null)
            {
                return BadRequest("Not Created Employe. Try Again!");
            }
            return Ok((EmployeeDto)createdEmploye);
        }

        // Put: api/Employe
        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var employe = _employeRepository.GetById(employeeDto.Guid);
            if (employe is null)
            {
                return NotFound("Id Not Found");
            }
            Employe toUpdate = employeeDto;
            toUpdate.CreatedDate = employe.CreatedDate;

            var updatedEmploye = _employeRepository.Update(employe);
            if (!updatedEmploye)
            {
                return BadRequest("Not Updated Employe. Try Again!");
            }
            return Ok("Data has been updated successfully");
        }

        // Delete: api/Employe
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var employe = _employeRepository.GetById(guid);
            if (employe is null)
            {
                return NotFound("Id Not Found");
            }
            var deletedEmploye = _employeRepository.Delete(employe);
            if (!deletedEmploye)
            {
                return BadRequest("Not Deleted Employe. Try Again!");
            }
            return Ok(deletedEmploye);
        }
    }
}
