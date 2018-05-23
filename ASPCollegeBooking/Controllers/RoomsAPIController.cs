using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.DTO;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;

namespace ASPCollegeBooking.Controllers
{
    [Produces("application/json")]
    [Route("api/RoomsAPI")]
    public class RoomsApiController : Controller
    {
        private readonly BookingContext _context;

        public RoomsApiController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/RoomsAPI
        [HttpGet]
        public IEnumerable GetRooms()
        {
            //we need to convert the rom ID from a string to an INT. So that when it gets ordered it is done with an int not a string
            //List<Rooms> allRooms = new List<Rooms>();
            //List<RoomsWithIntDTO> allRoomswithInt = new List<RoomsWithIntDTO>();
            ////display ordered by ID
            //allRooms.AddRange(_context.Rooms.OrderBy(r => r.ID).ThenBy(r => r.Title));

            //foreach (var room in allRooms)
            //{
            //    RoomsWithIntDTO newrooms = new RoomsWithIntDTO();
            //    newrooms.ID = Convert.ToInt32(room.ID);
            //    newrooms.Title = room.Title;
            //    newrooms.EventColor = room.EventColor;
            //    allRoomswithInt.Add(newrooms);
            //}

            OrderedRooms or = new OrderedRooms(_context);

            return or.GetOrderedRooms(); //allRoomswithInt.OrderBy(r => r.ID);
        }

        // GET: api/RoomsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRooms([FromRoute] int id)
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
        public async Task<IActionResult> PutRooms([FromRoute] int id, [FromBody] Rooms rooms)
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
                //if (!RoomsExists(id))
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
        public async Task<IActionResult> DeleteRooms([FromRoute] int id)
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

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}