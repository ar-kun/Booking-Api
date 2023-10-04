using Booking_Api.Contracts;
using Booking_Api.DTOs.Employees;
using Booking_Api.DTOs.Universities;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = universities.Select(x => (UniversityDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<UniversityDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var university = _universityRepository.GetById(guid);
            if (university is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<UniversityDto>((UniversityDto)university));
        }

        [HttpPost]
        public IActionResult Create(CreateUniversityDto createUniversityDto)
        {
            try { 
            var createdUniversity = _universityRepository.Create(createUniversityDto);
            
            return Ok(new ResponseOKHandler<UniversityDto>((UniversityDto)createdUniversity));
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

        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            try { 
            var university = _universityRepository.GetById(universityDto.Guid);
            if (university is null)
            {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

            University toUpdate = universityDto;
            toUpdate.CreatedDate = university.CreatedDate;

            _universityRepository.Update(university);
            
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

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var university = _universityRepository.GetById(guid);
                if (university is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                _universityRepository.Delete(university);
          
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
