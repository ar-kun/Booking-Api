using Booking_Api.Contracts;
using Booking_Api.DTOs.Rooms;
using Booking_Api.Models;
using Booking_Api.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
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
