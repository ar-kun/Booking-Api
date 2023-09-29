using Booking_Api.Contracts;
using Booking_Api.Models;
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

            return Ok(employes);
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
            return Ok(employe);
        }

        // Post: api/Employe
        [HttpPost]
        public IActionResult Create(Employees employe)
        {
            var createdEmploye = _employeRepository.Create(employe);
            if (createdEmploye is null)
            {
                return BadRequest("Not Created Employe. Try Again!");
            }
            return Ok(createdEmploye);
        }

        // Put: api/Employe
        [HttpPut]
        public IActionResult Update(Employees employe)
        {
            var updatedEmploye = _employeRepository.Update(employe);
            if (!updatedEmploye)
            {
                return BadRequest("Not Updated Employe. Try Again!");
            }
            return Ok(updatedEmploye);
        }

        // Delete: api/Employe
        [HttpDelete]
        public IActionResult Delete(Employees employe)
        {
            var deletedEmploye = _employeRepository.Delete(employe);
            if (!deletedEmploye)
            {
                return BadRequest("Not Deleted Employe. Try Again!");
            }
            return Ok(deletedEmploye);
        }
    }
}
