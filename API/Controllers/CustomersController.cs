using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly HotelContext _context;

        public CustomersController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            return await _context.Customer.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(uint id)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/5
        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<Customer>> GetCustomerEmailPassword(String email, String password)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.Where(item => item.email == email && item.password == password).ToListAsync();

            return customer == null || customer.Count() != 1 ? NotFound() : customer.First();
        }


        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(uint id, Customer customer)
        {
            if (id != customer.id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'HotelContext.Customer'  is null.");
            }

            var res = await _context.Customer.Where(item => item.email == customer.email).ToListAsync();

            if (res.Count > 0)
            {
                return Problem("email is being used");
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(uint id)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(uint id)
        {
            return (_context.Customer?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
