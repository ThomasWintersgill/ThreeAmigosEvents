using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Controllers
{
    public class GuestBookingsController : Controller
    {
        private readonly EventsDbContext _context;

        public GuestBookingsController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: GuestBookings
        public async Task<IActionResult> Index()
        {
            var eventsDbContext = _context.GuestBookings.Include(g => g.Event).Include(g => g.Guest);
            return View(await eventsDbContext.ToListAsync());
        }

        // GET: GuestBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GuestBookings == null)
            {
                return NotFound();
            }

            var guestBooking = await _context.GuestBookings
                .Include(g => g.Event)
                .Include(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (guestBooking == null)
            {
                return NotFound();
            }

            return View(guestBooking);
        }

        // GET: GuestBookings/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Events, "EventId", "EventId");
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestId", "GuestId");
            return View();
        }

        // POST: GuestBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,GuestID")] GuestBooking guestBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventId", "EventId", guestBooking.EventID);
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestId", "GuestId", guestBooking.GuestID);
            return View(guestBooking);
        }

        // GET: GuestBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GuestBookings == null)
            {
                return NotFound();
            }

            var guestBooking = await _context.GuestBookings.FindAsync(id);
            if (guestBooking == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventId", "EventId", guestBooking.EventID);
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestId", "GuestId", guestBooking.GuestID);
            return View(guestBooking);
        }

        // POST: GuestBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,GuestID")] GuestBooking guestBooking)
        {
            if (id != guestBooking.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookingExists(guestBooking.EventID))
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
            ViewData["EventID"] = new SelectList(_context.Events, "EventId", "EventId", guestBooking.EventID);
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestId", "GuestId", guestBooking.GuestID);
            return View(guestBooking);
        }

        // GET: GuestBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GuestBookings == null)
            {
                return NotFound();
            }

            var guestBooking = await _context.GuestBookings
                .Include(g => g.Event)
                .Include(g => g.Guest)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (guestBooking == null)
            {
                return NotFound();
            }

            return View(guestBooking);
        }

        // POST: GuestBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GuestBookings == null)
            {
                return Problem("Entity set 'EventsDbContext.GuestBookings'  is null.");
            }
            var guestBooking = await _context.GuestBookings.FindAsync(id);
            if (guestBooking != null)
            {
                _context.GuestBookings.Remove(guestBooking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestBookingExists(int id)
        {
          return _context.GuestBookings.Any(e => e.EventID == id);
        }
    }
}
