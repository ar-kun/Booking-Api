using Booking_Api.Contracts;
using Booking_Api.DTOs.Educations;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            var data = educations.Select(x => (EducationDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<EducationDto>>(data));
        }

        // Get: api/Education/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var education = _educationRepository.GetById(guid);
            if (education is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<EducationDto>((EducationDto)education));
        }

        // Post: api/Education
        [HttpPost]
        public IActionResult Create(CreateEducationDto createEducationDto)
        {
            try
            {
                var createdEducation = _educationRepository.Create(createEducationDto);
               
                return Ok(new ResponseOKHandler<EducationDto>((EducationDto)createdEducation));
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

        // Put: api/Education
        [HttpPut]
        public IActionResult Update(EducationDto educationDto)
        {
            try
            {
                var education = _educationRepository.GetById(educationDto.Guid);
                if (education is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                Education toUpdate = educationDto;
                toUpdate.CreatedDate = education.CreatedDate;

                _educationRepository.Update(education);
                
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

        // Delete: api/Education
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var education = _educationRepository.GetById(guid);
                if (education is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                _educationRepository.Delete(education);
                
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
