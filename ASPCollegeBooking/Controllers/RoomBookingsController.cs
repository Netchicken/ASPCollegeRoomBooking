using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;

namespace ASPCollegeBooking.Controllers
{
    public class RoomBookingsController : Controller
    {
        private readonly BookingContext _context;

        public RoomBookingsController(BookingContext context)
        {
            _context = context;
        }

        // GET: RoomBookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoomBookings.ToListAsync());
        }

        // GET: RoomBookings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .SingleOrDefaultAsync(m => m.BookingID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // GET: RoomBookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,ClientIDFK,From,Days,Weeks, Room")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings.SingleOrDefaultAsync(m => m.BookingID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookingID,ClientIDFK,From,Days,Weeks")] RoomBooking roomBooking)
        {
            if (id != roomBooking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomBookingExists(roomBooking.BookingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roomBooking);
        }

        // GET: RoomBookings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .SingleOrDefaultAsync(m => m.BookingID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roomBooking = await _context.RoomBookings.SingleOrDefaultAsync(m => m.BookingID == id);
            _context.RoomBookings.Remove(roomBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomBookingExists(string id)
        {
            return _context.RoomBookings.Any(e => e.BookingID == id);
        }
    }
}
