using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelContext _context;

        public RoomsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
        {
          if (_context.Room == null)
          {
              return NotFound();
          }
            return await _context.Room.Include(item => item.type).ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(uint id)
        {
          if (_context.Room == null)
          {
              return NotFound();
          }
            var room = await _context.Room.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // GET api/Rooms/RoomWithType/1/01-01-0001/01-01-0001
        [HttpGet("GetType/{typeId}")]
        public async Task<ActionResult<Room>> GetAvailableRoomWithType(uint typeId)
        {
            if (_context.Booking == null || _context.Room == null)
            {
                return NotFound();
            }


            DateTime start = DateTime.Today.AddHours(12);
            DateTime end = DateTime.Today.AddHours(12);

            /*****Get all bookings, include all objects within*****/
            List<Booking>? bookingList = await _context.Booking.Include(item => item.customer).Include(item => item.room).Include(item => item.room.type).ToListAsync();

            /*****Get all rooms*****/
            List<Room>? roomList = await _context.Room.Include(item => item.type).ToListAsync();

            foreach(Room room in roomList)
            {
                if (bookingList.Where(booking => booking.room == room).Count() > 0)
                {
                    roomList.Remove(room);
                }
            }

            if (roomList.Count <= 0)
            {
                return NotFound();
            }
            return roomList.First();

            /*
             bookinglist = getBookings();
             roomlist = getRooms();
             foreach item in roomlist 
                if(bookinglist.where(booking => booking.room == item).isnotempty) 
                    roomlist.remove(item)

             if(roomlist.isnotempty) 
                book
             else 
                ingen ledige rooms
             */
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(uint id, Room room)
        {
            if (id != room.id)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
          if (_context.Room == null)
          {
              return Problem("Entity set 'HotelContext.Room'  is null.");
          }
            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { room.id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(uint id)
        {
            if (_context.Room == null)
            {
                return NotFound();
            }
            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(uint id)
        {
            return (_context.Room?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
