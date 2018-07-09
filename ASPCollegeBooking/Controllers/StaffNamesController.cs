using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCollegeBooking.Data;
using ASPCollegeBooking.Models;
using ASPCollegeBooking.Business;

namespace ASPCollegeBooking.Controllers
{
    public class StaffNamesController : Controller
    {
        private readonly BookingContext _context;
        private readonly ITextFileOperations _textFileOperations;

        public StaffNamesController(BookingContext context, ITextFileOperations textFileOperations)
        {
            _context = context;
            _textFileOperations = textFileOperations;
        }


        public void UpdateStaff()
        {
            _textFileOperations.UploadStaffFile();

            //  return RedirectToAction(nameof(Index));

        }




        // GET: StaffNames
        public async Task<IActionResult> Index()
        {

            return View(await _context.StaffNames.ToListAsync());
        }

        // GET: StaffNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffNames = await _context.StaffNames
                .SingleOrDefaultAsync(m => m.Id == id);
            if (staffNames == null)
            {
                return NotFound();
            }

            return View(staffNames);
        }

        // GET: StaffNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Department,BookingCount")] StaffNames staffNames)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffNames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffNames);
        }

        // GET: StaffNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffNames = await _context.StaffNames.SingleOrDefaultAsync(m => m.Id == id);
            if (staffNames == null)
            {
                return NotFound();
            }
            return View(staffNames);
        }

        // POST: StaffNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Department,BookingCount")] StaffNames staffNames)
        {
            if (id != staffNames.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffNames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffNamesExists(staffNames.Id))
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
            return View(staffNames);
        }

        // GET: StaffNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffNames = await _context.StaffNames
                .SingleOrDefaultAsync(m => m.Id == id);
            if (staffNames == null)
            {
                return NotFound();
            }

            return View(staffNames);
        }

        // POST: StaffNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffNames = await _context.StaffNames.SingleOrDefaultAsync(m => m.Id == id);
            _context.StaffNames.Remove(staffNames);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffNamesExists(int id)
        {
            return _context.StaffNames.Any(e => e.Id == id);
        }
    }
}
