using Booking_Api.Contracts;
using Booking_Api.DTOs.Rooms;
using Booking_Api.Models;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound("Data Not Found");
            }
            var data = rooms.Select(x => (RoomDto)x);

            return Ok(data);
        }

        // GET: api/Room/guid
        [HttpGet("{guid}")]
        public IActionResult GetById(Guid guid)
        {
            var room = _roomRepository.GetById(guid);
            if (room is null)
            {
                return NotFound("Room Not found");
            }
            return Ok((RoomDto)room);
        }

        // POST: api/Room
        [HttpPost]
        public IActionResult Create(CreateRoomDto createRoomDto)
        {
            var createdRoom = _roomRepository.Create(createRoomDto);
            if (createdRoom is null)
            {
                return BadRequest("Not Created Room. Try Again!");
            }
            return Ok((RoomDto)createdRoom);
        }

        // PUT: api/Room
        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            var room = _roomRepository.GetById(roomDto.Guid);
            if (room is null)
            {
                return NotFound("Id Not Found");
            }

            Room toUpdate = roomDto;
            toUpdate.CreatedDate = room.CreatedDate;

            var updatedRoom = _roomRepository.Update(room);
            if (!updatedRoom)
            {
                return BadRequest("Not Updated Room. Try Again!");
            }
            return Ok("Data has been updated successfully");
        }

        // DELETE: api/Room
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var room = _roomRepository.GetById(guid);
            if (room is null)
            {
                return NotFound("Id Not Found");
            }
            var deletedRoom = _roomRepository.Delete(room);
            if (!deletedRoom)
            {
                return BadRequest("Not Deleted Room. Try Again!");
            }
            return Ok(deletedRoom);
        }
    }
}
