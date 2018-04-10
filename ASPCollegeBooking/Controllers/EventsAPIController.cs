using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace ASPCollegeBooking.Controllers
{
    [Produces("application/json")]
    [Route("api/EventsApi")]
    public class EventsApiController : Controller
    {
        private readonly BookingContext _context;

        public EventsApiController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/EventsApi
        [HttpGet]
        public IEnumerable<Events> GetEvents()
        {
            DayWeeksAllDayMods();

            //   DbSet<Events> FinalEvents = new DbSet<Events>(Allbookings);
          //              FinalEvents.AddRange(Allbookings);
            return _context.Events;
        }

        private void DayWeeksAllDayMods()
        {
//need to change to a list instead of a DB set to add in new data
            var Allbookings = new List<Events>(_context.Events);
            //temporary list to hold new data and merge at end
            var newbookings = new List<Events>();

            foreach (var booking in Allbookings)
            {
                if (booking.IsFullDay == true)
                {
                    //todo create a start and end time for the dates saved
                    //  booking.Start = booking.Start.Date +date
                }

                if (booking.Days > 0)
                {
                    //todo create new bookings for the amount of days - account for weekends
                    //  booking.Start = booking.Start.Date +date

                    for (int i = 0; i < booking.Days; i++)
                    {
                        booking.Start = booking.Start.AddDays(1);
                        booking.End = booking.End.AddDays(1);

                        //if the booking is a saturday add 2 days to make it a monday
                        if (booking.Start.DayOfWeek == DayOfWeek.Saturday)
                        {
                            booking.Start = booking.Start.AddDays(2);
                            booking.End = booking.End.AddDays(2);
                        }
                        //add the new booking repeat for each day

                        newbookings.Add(booking);
                        // _context.Events.Add(newbooking);
                    }
                }

                if (booking.Weeks > 0)
                {
                    //todo create new bookings for the amount of weeks 
                }
            }

            Allbookings.AddRange(newbookings);
        }

        // GET: api/EventsApi/5
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

        // PUT: api/EventsApi/5
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

        // POST: api/EventsApi
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

        // DELETE: api/EventsApi/5
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