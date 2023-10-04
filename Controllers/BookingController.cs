using Booking_Api.Contracts;
using Booking_Api.Models;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound("Data Not Found");
            }

            return Ok(bookings);
        }

        // Get: api/Booking/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var booking = _bookingRepository.GetById(guid);
            if (booking is null)
            {
                return NotFound("Booking Not found");
            }
            return Ok(booking);
        }

        // Post: api/Booking
        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            var createdBooking = _bookingRepository.Create(booking);
            if (createdBooking is null)
            {
                return BadRequest("Not Created Booking. Try Again!");
            }
            return Ok(createdBooking);
        }

        // Put: api/Booking
        [HttpPut]
        public IActionResult Update(Booking booking)
        {
            var updatedBooking = _bookingRepository.Update(booking);
            if (!updatedBooking)
            {
                return BadRequest("Not Updated Booking. Try Again!");
            }
            return Ok(updatedBooking);
        }

        // Delete: api/Booking
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var booking = new Booking() { Guid = guid };
            var deletedBooking = _bookingRepository.Delete(booking);
            if (!deletedBooking)
            {
                return BadRequest("Not Deleted Booking. Try Again!");
            }
            return Ok(deletedBooking);
        }
    }
}
