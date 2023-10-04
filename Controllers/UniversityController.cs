using Booking_Api.Contracts;
using Booking_Api.DTOs.Universities;
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

            var data = universities.Select(x => (UniversityDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var university = _universityRepository.GetById(guid);
            if (university is null)
            {
                return NotFound("University Not found");
            }
            return Ok((UniversityDto)university);
        }

        [HttpPost]
        public IActionResult Create(CreateUniversityDto createUniversityDto)
        {
            var createdUniversity = _universityRepository.Create(createUniversityDto);
            if (createdUniversity is null)
            {
                return BadRequest("Not Created University. Try Again!");
            }
            return Ok((UniversityDto)createdUniversity);
        }

        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var university = _universityRepository.GetById(universityDto.Guid);
            if (university is null)
            {
                return NotFound("Id Not Found");
            }

            University toUpdate = universityDto;
            toUpdate.CreatedDate = university.CreatedDate;

            var updatedUniversity = _universityRepository.Update(university);
            if (!updatedUniversity)
            {
                return BadRequest("Not Updated University. Try Again!");
            }
            return Ok("Data has been updated successfully");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
           
            var university = _universityRepository.GetById(guid);

            var deletedUniversity = _universityRepository.Delete(university);
            if (!deletedUniversity)
            {
                return BadRequest("Not Deleted University. Try Again!");
            }
            return Ok(deletedUniversity);
        }
    }
}
