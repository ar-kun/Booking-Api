using Booking_Api.Contracts;
using Booking_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var universities = _universityRepository.GetAll();
            if (!universities.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(universities);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var university = _universityRepository.GetById(guid);
            if (university is null)
            {
                return NotFound("University Not found");
            }
            return Ok(university);
        }

        [HttpPost]
        public IActionResult Create(University university)
        {
            var createdUniversity = _universityRepository.Create(university);
            if (createdUniversity is null)
            {
                return BadRequest("Not Created University. Try Again!");
            }
            return Ok(createdUniversity);
        }

        [HttpPut]
        public IActionResult Update(University university)
        {
            var updatedUniversity = _universityRepository.Update(university);
            if (!updatedUniversity)
            {
                return BadRequest("Not Updated University. Try Again!");
            }
            return Ok(updatedUniversity);
        }

        [HttpDelete]
        public IActionResult Delete(University university)
        {
            var deletedUniversity = _universityRepository.Delete(university);
            if (!deletedUniversity)
            {
                return BadRequest("Not Deleted University. Try Again!");
            }
            return Ok(deletedUniversity);
        }
    }
}
