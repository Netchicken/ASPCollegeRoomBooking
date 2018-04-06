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
    public class EventsController : Controller
    {
        private readonly BookingContext _context;

        public EventsController(BookingContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {

            // var roomID = new SelectList(_context.Rooms.Select(r => new {ID = r.ID}).ToList());


            //  List<Rooms> allrooms = new List<Rooms>();
            Dictionary<string, string> allroomsdic = new Dictionary<string, string>();

            foreach (var room in _context.Rooms)
            {
                //find all the data in the reasource id that matches the room ID. then foreach it adding the room title to the ID
                //https://stackoverflow.com/questions/37075142/linq-replace-column-value-with-another-value (this is beautiful)
                _context.Events.Where(e => e.ResourceId.Equals(room.ID)).ToList().ForEach(i => i.ResourceId = i.ResourceId + " " + room.Title);
            }



            return View(await _context.Events.ToListAsync());
        }

        public IActionResult Scheduler()
        {

            return View();
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .SingleOrDefaultAsync(m => m.ID == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Events/Create
        public IActionResult Create()
        {

            // ViewData["Roomlist"] = new SelectList(_context.Rooms, "ID", "Title").ToList();

            //https://www.learnrazorpages.com/razor-pages/tag-helpers/select-tag-helper
            //ViewData["Roomlist"] = _context.Rooms
            //   .Select(n => new SelectListItem
            //   {
            //       Value = n.ID.ToString(),
            //       Text = n.Title.ToString()
            //   }).ToList();

            ViewBag.Roomlist = new SelectList(_context.Rooms, "ID", "Title");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ResourceId,EventColor,Start,End,Title,RoomID")] Events events)
        {
            if (ModelState.IsValid)
            {
                // events.Room.ID = events.ResourceId;
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.ID == id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ResourceId,EventColor,Start,End,Title")] Events events)
        {
            if (id != events.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("About", "Home");
            }
            return RedirectToAction("About", "Home");
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .SingleOrDefaultAsync(m => m.ID == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var events = await _context.Events.SingleOrDefaultAsync(m => m.ID == id);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsExists(string id)
        {
            return _context.Events.Any(e => e.ID == id);
        }
    }
}
