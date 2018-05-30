using System;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.DTO;
using ASPCollegeBooking.Email;
using ASPCollegeBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ASPCollegeBooking.Controllers
{
    public class EventsController : Controller
    {
        private readonly BookingContext _context;
        private readonly OrderedRooms or;
        public UserManager<ApplicationUser> UserManager { get; }

        public EventsController(BookingContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            or = new OrderedRooms(_context);
            UserManager = userManager;
        }
        //event checking in calendar done in the EventsAPIController

        // GET: Events
        public async Task<IActionResult> Index()
        {
            foreach (var room in _context.Rooms)
            {
                //find all the data in the reasource id that matches the room ID. then foreach it adding the room title to the ID
                //https://stackoverflow.com/questions/37075142/linq-replace-column-value-with-another-value (this is beautiful)
                _context.Events.Where(e => e.ResourceId.Equals(room.ID)).OrderBy(r => r.Room.ID).ToList().ForEach(i => i.ResourceId = i.ResourceId + " - " + room.Title);
            }
            return View(await _context.Events.OrderByDescending(e => e.Id).ToListAsync());
        }

        public IActionResult Scheduler()
        {
            return View();
        }


        public IActionResult Email()
        {
            ViewBag.Emailresponse = "Before sending - ";
            try
            {
            var message=    SendSimpleMessageChunk.SendSimpleMessage().Content.ToString();
                ViewBag.Emailresponse += "Simple Message sent - - - " + message;
            }
            catch (Exception e)
            {
                //catch errors
                ViewBag.Emailresponse += "Simple errors " + e.ToString();
            }

            //try
            //{
            //    SendSMTP.SendMessageSmtp();
            //    ViewBag.Emailresponse += " SMTP sent ";
            //}
            //catch (Exception e)
            //{
            //    //catch errors
            //    ViewBag.Emailresponse += " SMTP errors " + e.ToString();
            //}

            return View();
        }


        // GET: Events/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var events = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        [Authorize]
        // GET: Events/Create
        public IActionResult Create()
        {
            ViewBag.TodayDate = DateTime.Today.ToLongDateString();
            ViewBag.Roomlist = new SelectList(or.GetOrderedRooms(), "ID", "Title");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ResourceId,EventColor,Start,End,Title,RoomID,IsFullDay,Days,Weeks,Email")] Events events)
        {
            ViewBag.TodayDate = DateTime.Today.ToLongDateString();
            ViewBag.Roomlist = new SelectList(or.GetOrderedRooms(), "ID", "Title");
            //get user details save to db
            string UserEmail = User.Identity.Name;
            string[] details = UserEmail.Split('@');
            //pass it back to the event in the SchedularCustom.js
            events.Name = details[0];
            events.Email = UserEmail;

            //is the start date older than the end date?
            if (ModelState.IsValid)
            {
                //check if there is a fullday or repeating weeks or days
                if (events.IsFullDay || events.Days > 0 || events.Weeks > 0)
                {
                    foreach (var booking in DayWeeksAllDayMods.EventCalc(events))
                    {
                        _context.Add(booking);
                    }
                }
                //pass through the event and look for clashes where its the same room after today
                DayWeeksAllDayMods.DoTheDatesOverlap(_context.Events.Where(e => e.ResourceId == events.ResourceId && e.End > DateTime.Now).ToList(), events);

                //We have a clash
                if (DayWeeksAllDayMods.WeHaveAClash == true)
                {
                    return RedirectToAction("Clash");
                }
                else
                {//we dont have a clash
                    _context.Add(events);
                    await _context.SaveChangesAsync();
                    return View();
                }
            }
            else
            {
                //if model is not valid return view - have to refresh context
                ViewBag.Roomlist = new SelectList(or.GetOrderedRooms(), "ID", "Title");
                return View();
            }
        }

        public IActionResult Clash()
        {
            ClashDTO myClashDTO = new ClashDTO();
            myClashDTO.BookingClashDic = DayWeeksAllDayMods.BookingClashDic;
            return View(myClashDTO);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Roomlist = new SelectList(or.GetOrderedRooms(), "ID", "Title");

            var events = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ResourceId,EventColor,Start,End,Title")] Events events)
        {
            if (id != events.Id)
            {
                //warning you can change the edit/ID in the task bar and save it back ss
                events.Id = id;
                //  return NotFound();
            }

            if (ModelState.IsValid && events.End > events.Start)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.Id))
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
            return RedirectToAction(nameof(Index));
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var events = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var events = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
