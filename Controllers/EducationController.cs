using Booking_Api.Contracts;
using Booking_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {

        private readonly IEducationRepository _educationRepository;

        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        // Get: api/Education
        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _educationRepository.GetAll();
            if (!educations.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(educations);
        }

        // Get: api/Education/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var education = _educationRepository.GetById(guid);
            if (education is null)
            {
                return NotFound("Education Not found");
            }
            return Ok(education);
        }

        // Post: api/Education
        [HttpPost]
        public IActionResult Create(Educations education)
        {
            var createdEducation = _educationRepository.Create(education);
            if (createdEducation is null)
            {
                return BadRequest("Not Created Education. Try Again!");
            }
            return Ok(createdEducation);
        }

        // Put: api/Education
        [HttpPut]
        public IActionResult Update(Educations education)
        {
            var updatedEducation = _educationRepository.Update(education);
            if (!updatedEducation)
            {
                return BadRequest("Not Updated Education. Try Again!");
            }
            return Ok(updatedEducation);
        }

        // Delete: api/Education
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var result = new Educations() { Guid = guid };
            var deletedEducation = _educationRepository.Delete(result);
            if (!deletedEducation)
            {
                return BadRequest("Not Deleted Education. Try Again!");
            }
            return Ok(deletedEducation);
        }
    }
}
