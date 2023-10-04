using Booking_Api.Contracts;
using Booking_Api.DTOs.Bookings;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // Get: api/Booking
        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            var data = bookings.Select(x => (BookingDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<BookingDto>>(data));
        }

        // Get: api/Booking/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var booking = _bookingRepository.GetById(guid);
            if (booking is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<BookingDto>((BookingDto)booking));
        }

        // Post: api/Booking
        [HttpPost]
        public IActionResult Create(CreateBookingDto createBookingDto)
        {
            try
            {
                var createdBooking = _bookingRepository.Create(createBookingDto);
                
                return Ok(new ResponseOKHandler<BookingDto>((BookingDto)createdBooking));
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

        // Put: api/Booking
        [HttpPut]
        public IActionResult Update(BookingDto bookingDto)
        {
            try
            {
                var booking = _bookingRepository.GetById(bookingDto.Guid);
                if (booking is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                Booking toUpdate = bookingDto;
                toUpdate.CreatedDate = booking.CreatedDate;

                 _bookingRepository.Update(booking);
                
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

        // Delete: api/Booking
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var booking = _bookingRepository.GetById(guid);
                if (booking is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                _bookingRepository.Delete(booking);
                
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
