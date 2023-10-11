﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Models;
using System.Globalization;

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

		// GET api/Rooms/RoomWithType/1/2023-10-12T12:00:00Z/2023-10-12T12:00:00Z
		[HttpGet("GetType/{typeId}/{start}/{end}")]
        public async Task<ActionResult<Room>> GetAvailableRoomWithType(uint typeId, String start, String end)
        {
            await RemoveOldBooking();
            if (_context.Booking == null || _context.Room == null)
            {
                return NotFound();
            }

            
			DateTime startDate = DateTime.ParseExact(start.Replace(".", ":"), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
			DateTime endDate = DateTime.ParseExact(end.Replace(".", ":"), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);


            /*****Get all bookings, include all objects within*****/
            List<Booking>? bookingList = await _context.Booking.Include(item => item.customer).Include(item => item.room).Include(item => item.room.type).ToListAsync();

            /*****Get all rooms, Make list for all usefull rooms*****/
            List<Room>? roomList = await _context.Room.Include(item => item.type).Where(item => item.typeId == typeId).ToListAsync();
            List<Room> finalRooms = new();

			foreach (Room room in roomList)
            {
                if (bookingList.Where(booking => booking.room == room && startDate < booking.endDate && endDate > booking.startDate).Count() < 1)
                {
                    finalRooms.Add(room);
                }
			}

            if (finalRooms.Count < 1)
            {
                return NotFound();
            }
            return finalRooms.First();
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

		private async Task RemoveOldBooking()
		{
			List<Booking>? bookings = await _context.Booking.Include(item => item.customer).Include(item => item.room).Include(item => item.room.type)?.ToListAsync();
			if (bookings == null && bookings.Count > 0)
			{
				return;
			}
			foreach (Booking booking in bookings)
			{
				if (booking.endDate.Date <= DateTime.Today.AddHours(booking.endDate.Hour))
				{
					_context.Booking.Remove(booking);
				}
			}
			await _context.SaveChangesAsync();
			return;
		}
	}
}
