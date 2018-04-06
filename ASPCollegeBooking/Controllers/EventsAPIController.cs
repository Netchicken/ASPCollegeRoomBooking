using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.DTO;
using ASPCollegeBooking.Models;

namespace ASPCollegeBooking.Controllers
{
    [Produces("application/json")]
    [Route("api/EventsAPI")]
    public class EventsAPIController : Controller
    {
        private readonly BookingContext _context;

        public EventsAPIController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/EventsAPI
        [HttpGet]
        public IEnumerable<Events> GetEvents()
        {
            return _context.Events;
        }

        // GET: api/EventsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvents([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.ID == id);

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        //The HTTP PATCH method should be used whenever you would like to change or update just a small part of the state of the resource. You should use the PUT method only when you would like to replace the resource in its entirety. Note that PATCH is not a replacement for PUT or POST, but just a way of applying a delta update to a resource representation.

        // PUT: api/EventsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvents([FromRoute] string id, [FromBody] Events events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != events.ID)
            {
                return BadRequest();
            }

            _context.Entry(events).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsExists(id))
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

        // POST: api/EventsAPI
        [HttpPost]
        public async Task<IActionResult> PostEvents([FromBody] Events events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Events.Add(events);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvents", new { id = events.ID }, events);
        }

        // DELETE: api/EventsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvents([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.ID == id);
            if (events == null)
            {
                return NotFound();
            }

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

            return Ok(events);
        }

        private bool EventsExists(string id)
        {
            return _context.Events.Any(e => e.ID == id);
        }
    }
}