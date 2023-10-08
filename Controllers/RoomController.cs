using Booking_Api.Contracts;
using Booking_Api.DTOs.Rooms;
using Booking_Api.Models;
using Booking_Api.Repositories;
using Booking_Api.Utilities.Handler;
using Booking_Api.Utilities.Validations.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeRepository _employeRepository;

        public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository, IEmployeRepository employeRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _employeRepository = employeRepository;
        }

        //Room used
        [HttpGet("used")]
        public IActionResult GetUsedRooms()
        {
            try {
                var rooms = _roomRepository.GetAll();
                var bookings = _bookingRepository.GetAll();
                var employees = _employeRepository.GetAll();

                if (bookings is null && employees is null && rooms is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                var usedRooms = from r in rooms
                                join b in bookings on r.Guid equals b.RoomGuid
                                join e in employees on b.EmployeeGuid equals e.Guid
                                where (b.StartDate <= DateTime.Today) && (b.EndDate > DateTime.Today)
                                select new UsedRoomDto
                                {
                                        BookingGuid = b.Guid,
                                        RoomName = r.Name,
                                        Status = b.Status.ToString(),
                                        Floor = r.Floor,
                                        BookBy = string.Concat(e.FirstName, " ", e.LastName)
                                };
                return Ok(new ResponseOKHandler<IEnumerable<UsedRoomDto>>(usedRooms));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to get used rooms",
                    Error = ex.Message
                });
            }
        }
        // GET: api/Room
        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            var data = rooms.Select(x => (RoomDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>(data));
        }

        // GET: api/Room/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var room = _roomRepository.GetById(guid);
            if (room is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseOKHandler<RoomDto>((RoomDto)room));
        }

        // POST: api/Room
        [HttpPost]
        public IActionResult Create(CreateRoomDto createRoomDto)
        {
            try
            {
                var createdRoom = _roomRepository.Create(createRoomDto);
                
                return Ok(new ResponseOKHandler<RoomDto>((RoomDto)createdRoom));
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

        // PUT: api/Room
        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            try
            {
                var room = _roomRepository.GetById(roomDto.Guid);
                if (room is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                Room toUpdate = roomDto;
                toUpdate.CreatedDate = room.CreatedDate;

                _roomRepository.Update(room);
                
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

        // DELETE: api/Room
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var room = _roomRepository.GetById(guid);
                if (room is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                _roomRepository.Delete(room);
                
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
