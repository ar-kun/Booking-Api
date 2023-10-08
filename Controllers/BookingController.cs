using Booking_Api.Contracts;
using Booking_Api.DTOs.Bookings;
using Booking_Api.Models;
using Booking_Api.Repositories;
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
        private readonly IEmployeRepository _employeRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingController(IBookingRepository bookingRepository, IEmployeRepository employeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _employeRepository = employeRepository;
            _roomRepository = roomRepository;
        }

        //DetailBooking
        [HttpGet("booking-details")]
        public IActionResult GetDetails()
        {
            try
            {
                var bookings = _bookingRepository.GetAll();
                var employees = _employeRepository.GetAll();
                var rooms = _roomRepository.GetAll();

                if (bookings is null && employees is null && rooms is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                var details = from b in bookings
                              join e in employees on b.EmployeeGuid equals e.Guid
                              join r in rooms on b.RoomGuid equals r.Guid
                              select new DetailBookingDto
                              {
                                  Guid = b.Guid,
                                  BookedNIK = e.Nik,
                                  BookedBy = string.Concat(e.FirstName, " ", e.LastName),
                                  RoomName = r.Name,
                                  StartDate = b.StartDate,
                                  EndDate = b.EndDate,
                                  Status = b.Status.ToString(),
                                  Remarks = b.Remarks
                              };
                return Ok(new ResponseOKHandler<IEnumerable<DetailBookingDto>>(details));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to get details booking",
                    Error = ex.Message
                });
            }
        }

        //Detail by Guid
        [HttpGet("booking-detail/{guid}")]
        public IActionResult GetDetail(Guid guid)
        {
            try
            {
                var bookings = _bookingRepository.GetAll();
                var employees = _employeRepository.GetAll();
                var rooms = _roomRepository.GetAll();

                if (bookings is null && employees is null && rooms is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                var detailByGuid = from b in bookings
                                   join e in employees on b.EmployeeGuid equals e.Guid
                                   join r in rooms on b.RoomGuid equals r.Guid
                                   where b.Guid == guid
                                   select new DetailBookingDto
                                   {
                                       Guid = guid,
                                       BookedNIK = e.Nik,
                                       BookedBy = string.Concat(e.FirstName, " ", e.LastName),
                                       RoomName = r.Name,
                                       StartDate = b.StartDate,
                                       EndDate = b.EndDate,
                                       Status = b.Status.ToString(),
                                       Remarks = b.Remarks
                                   };
                return Ok(new ResponseOKHandler<IEnumerable<DetailBookingDto>>(detailByGuid));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to get detail booking",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("time-used")]
        public IActionResult GetTimeBooking()
        {
            try
            {
                var bookings = _bookingRepository.GetAll();
                var rooms = _roomRepository.GetAll();
                if (bookings is null && rooms is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                var bookingTime = from b in bookings
                                  join r in rooms on b.RoomGuid equals r.Guid
                                  select new TimeBookingDto
                                  {
                                      RoomGuid = b.Guid,
                                      RoomName = r.Name,
                                      BookingTime = (int)b.EndDate.Subtract(b.StartDate).TotalHours
                                  };
                return Ok(new ResponseOKHandler<IEnumerable<TimeBookingDto>>(bookingTime));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to get time used booing",
                    Error = ex.Message
                });
            }
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
