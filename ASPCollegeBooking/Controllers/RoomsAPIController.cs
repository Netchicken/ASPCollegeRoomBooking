using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;

namespace ASPCollegeBooking.Controllers
{
    [Produces("application/json")]
    [Route("api/RoomsAPI")]
    public class RoomsAPIController : Controller
    {
        private readonly BookingContext _context;

        public RoomsAPIController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/RoomsAPI
        [HttpGet]
        public IEnumerable<Rooms> GetRooms()
        {
            return _context.Rooms;
        }

        // GET: api/RoomsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRooms([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rooms = await _context.Rooms.SingleOrDefaultAsync(m => m.ID == id);

            if (rooms == null)
            {
                return NotFound();
            }

            return Ok(rooms);
        }

        // PUT: api/RoomsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRooms([FromRoute] string id, [FromBody] Rooms rooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rooms.ID)
            {
                return BadRequest();
            }

            _context.Entry(rooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomsExists(id))
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

        // POST: api/RoomsAPI
        [HttpPost]
        public async Task<IActionResult> PostRooms([FromBody] Rooms rooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Rooms.Add(rooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRooms", new { id = rooms.ID }, rooms);
        }

        // DELETE: api/RoomsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRooms([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rooms = await _context.Rooms.SingleOrDefaultAsync(m => m.ID == id);
            if (rooms == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();

            return Ok(rooms);
        }

        private bool RoomsExists(string id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}