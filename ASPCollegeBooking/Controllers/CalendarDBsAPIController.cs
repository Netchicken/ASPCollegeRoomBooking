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
    [Route("api/CalendarDBsAPI")]
    public class CalendarDBsAPIController : Controller
    {
        private readonly BookingContext _context;

        public CalendarDBsAPIController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/CalendarDBsAPI
        [HttpGet]
        public IEnumerable<CalendarDB> GetCalendarDbs()
        {
            return _context.CalendarDbs;
        }

        // GET: api/CalendarDBsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCalendarDB([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calendarDB = await _context.CalendarDbs.SingleOrDefaultAsync(m => m.EventID == id);

            if (calendarDB == null)
            {
                return NotFound();
            }

            return Ok(calendarDB);
        }

        // PUT: api/CalendarDBsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendarDB([FromRoute] string id, [FromBody] CalendarDB calendarDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calendarDB.EventID)
            {
                return BadRequest();
            }

            _context.Entry(calendarDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarDBExists(id))
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

        // POST: api/CalendarDBsAPI
        [HttpPost]
        public async Task<IActionResult> PostCalendarDB([FromBody] CalendarDB calendarDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CalendarDbs.Add(calendarDB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendarDB", new { id = calendarDB.EventID }, calendarDB);
        }

        // DELETE: api/CalendarDBsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarDB([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calendarDB = await _context.CalendarDbs.SingleOrDefaultAsync(m => m.EventID == id);
            if (calendarDB == null)
            {
                return NotFound();
            }

            _context.CalendarDbs.Remove(calendarDB);
            await _context.SaveChangesAsync();

            return Ok(calendarDB);
        }

        private bool CalendarDBExists(string id)
        {
            return _context.CalendarDbs.Any(e => e.EventID == id);
        }
    }
}