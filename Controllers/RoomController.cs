using Booking_Api.Contracts;
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

            return Ok(rooms);
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
            return Ok(room);
        }

        // POST: api/Room
        [HttpPost]
        public IActionResult Create(Rooms room)
        {
            var createdRoom = _roomRepository.Create(room);
            if (createdRoom is null)
            {
                return BadRequest("Not Created Room. Try Again!");
            }
            return Ok(createdRoom);
        }

        // PUT: api/Room
        [HttpPut]
        public IActionResult Update(Rooms room)
        {
            var updatedRoom = _roomRepository.Update(room);
            if (!updatedRoom)
            {
                return BadRequest("Not Updated Room. Try Again!");
            }
            return Ok(updatedRoom);
        }

        // DELETE: api/Room
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var result = new Rooms() { Guid = guid };
            var deletedRoom = _roomRepository.Delete(result);
            if (!deletedRoom)
            {
                return BadRequest("Not Deleted Room. Try Again!");
            }
            return Ok(deletedRoom);
        }
    }
}
