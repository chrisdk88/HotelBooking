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
    public class BookingsController : ControllerBase
    {
        private readonly HotelContext _context;

        public BookingsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            RemoveOldBooking();
            List<Booking> bookings = await _context.Booking.Include(item => item.customer).Include(item => item.room).Include(item => item.room.type).ToListAsync();

            if (bookings == null || bookings.Count < 1) 
            {
                return NotFound();
            }

            return bookings;
        }

		// GET: api/Bookings/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(uint id)
        {
          RemoveOldBooking();
          if (_context.Booking == null)
          {
              return NotFound();
          }
            var booking = await _context.Booking.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // GET: api/Bookings/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingFromUserID(uint id)
        {
            RemoveOldBooking();
            if (_context.Booking == null)
            {
                return NotFound();
            }

            var bookings = await _context.Booking.Include(item => item.customer).Include(item => item.room).Include(item => item.room.type).ToListAsync();
            if (bookings == null || bookings.Count <= 0)
            {
                return NotFound();
            }

            var bookingsFromCustomer = bookings.Where(item => item.customerid == id).ToList();

            if (bookingsFromCustomer == null || bookingsFromCustomer.Count() <= 0)
            {
                return NotFound();
            }

            return bookingsFromCustomer;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(uint id, Booking booking)
        {
            RemoveOldBooking();

			if (id != booking.id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
          RemoveOldBooking();
          if (_context.Booking == null)
          {
              return Problem("Entity set 'HotelContext.Booking'  is null.");
          }
            if (_context.Customer.FirstOrDefault(item => item.id == booking.customerid) == null)
            {
                return BadRequest("Customer does not exist");
            }
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(uint id)
        {
            if (_context.Booking == null)
            {
                return NotFound();
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            RemoveOldBooking();

            return NoContent();
        }

        private bool BookingExists(uint id)
        {
            return (_context.Booking?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private async void RemoveOldBooking()
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
