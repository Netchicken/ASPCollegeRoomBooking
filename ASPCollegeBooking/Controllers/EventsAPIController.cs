﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;
using Microsoft.AspNetCore.Identity;
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

            return _context.Events;
        }

        //// GET: api/GetUserEmail
        //[HttpGet]
        //public string GetUserEmail()
        //{
        //    return User.Identity.Name;

        //}

        // GET: api/EventsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        // PUT: api/EventsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvents([FromRoute] int id, [FromBody] Events events)
        {
            //todo add in user check here

            string UserEmail = User.Identity.Name;

            if (!ModelState.IsValid || UserEmail == null || events.Email != UserEmail)
            {
                return BadRequest(ModelState);
            }

            if (id != events.Id)
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
                //if (!EventsExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }
        //todo this is not being triggered in Create page
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

            return CreatedAtAction("GetEvents", new { id = events.Id }, events);
        }

        // DELETE: api/EventsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (events == null)
            {
                return NotFound();
            }

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

            return Ok(events);
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}