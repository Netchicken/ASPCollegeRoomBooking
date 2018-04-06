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
    public class CalendarDBsController : Controller
    {
        private readonly BookingContext _context;

        public CalendarDBsController(BookingContext context)
        {
            _context = context;
        }

      // await _context.CalendarDbs.ToListAsync()
        public  JsonResult Index()
        {
            var result = Json(_context.CalendarDbs.ToListAsync());

            return Json(result);

        }

        // GET: CalendarDBs
        public JsonResult GetEvents()
        {
            return  Json( _context.CalendarDbs.ToListAsync());
         
            }
        


        // GET: CalendarDBs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarDB = await _context.CalendarDbs
                .SingleOrDefaultAsync(m => m.EventID == id);
            if (calendarDB == null)
            {
                return NotFound();
            }

            return View(calendarDB);
        }

        // GET: CalendarDBs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalendarDBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,Subject,Description,Start,End,ThemeColor,IsFullDay,delete")] CalendarDB calendarDB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendarDB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calendarDB);
        }

        // GET: CalendarDBs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarDB = await _context.CalendarDbs.SingleOrDefaultAsync(m => m.EventID == id);
            if (calendarDB == null)
            {
                return NotFound();
            }
            return View(calendarDB);
        }

        // POST: CalendarDBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EventID,Subject,Description,Start,End,ThemeColor,IsFullDay,delete")] CalendarDB calendarDB)
        {
            if (id != calendarDB.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendarDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarDBExists(calendarDB.EventID))
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
            return View(calendarDB);
        }

        // GET: CalendarDBs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarDB = await _context.CalendarDbs
                .SingleOrDefaultAsync(m => m.EventID == id);
            if (calendarDB == null)
            {
                return NotFound();
            }

            return View(calendarDB);
        }

        // POST: CalendarDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var calendarDB = await _context.CalendarDbs.SingleOrDefaultAsync(m => m.EventID == id);
            _context.CalendarDbs.Remove(calendarDB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarDBExists(string id)
        {
            return _context.CalendarDbs.Any(e => e.EventID == id);
        }
    }
}
