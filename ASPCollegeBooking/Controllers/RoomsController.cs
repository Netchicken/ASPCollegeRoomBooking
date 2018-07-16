using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASPCollegeBooking.Controllers
{
    public class RoomsController : Controller
    {
        private readonly BookingContext _context;

        public RoomsController(BookingContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var rooms = await _context.Rooms
                .SingleOrDefaultAsync(m => m.ID == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //Guid are messing up the room bookings BUT I can't turnit all to ints, so I generate a new int from the max of the existing number +1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,EventColor,IsBookable,MaxOccupancy")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                //  List<int> ListRoomID = new List<int>();
                //foreach (var room in _context.Rooms)
                //{
                //    ListRoomID.Add(Convert.ToInt16(room.ID));
                //}
                //  _context.Rooms.ToList().ForEach(r => ListRoomID.Add(Convert.ToInt16(r.ID)));

                //var NewID = _context.Rooms.Select(r => Convert.ToInt16(r.ID.Max())+1).ToString();

                //  rooms.ID = ListRoomID.Max() + 1; 

                _context.Add(rooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rooms);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var rooms = await _context.Rooms.SingleOrDefaultAsync(m => m.ID == id);
            if (rooms == null)
            {
                return NotFound();
            }
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,EventColor,IsBookable,MaxOccupancy")] Rooms rooms)
        {
            if (id != rooms.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!RoomsExists(rooms.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rooms);
        }
        [Authorize]
        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var rooms = await _context.Rooms
                .SingleOrDefaultAsync(m => m.ID == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rooms = await _context.Rooms.SingleOrDefaultAsync(m => m.ID == id);
            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
