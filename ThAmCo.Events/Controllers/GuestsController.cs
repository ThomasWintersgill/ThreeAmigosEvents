using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;


namespace ThAmCo.Events.Controllers
{
    public class GuestsController : Controller
    {

        #region DBcontextInjection
        private readonly EventsDbContext _context;

        public GuestsController(EventsDbContext context)
        {
            _context = context;
        }
        #endregion

        #region CRUD
        // GET: Guests
        public async Task<IActionResult> Index()
        {
            var model = await _context.Guests.ToListAsync();
            var viewModel = model.Select(g => new GuestViewModel
            {
                GuestId = g.GuestId,
                Forename = g.Forename,
                Surname = g.Surname,

            });
              return View(viewModel);
        }

        // GET: Guests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

       
            var guestViewModel = await _context.Guests.Select(b => new GuestViewModel()
            {
                GuestId = b.GuestId,
                Forename= b.Forename,
                Surname= b.Surname,
                ContactNumber = b.ContactNumber,
                ContactAdress = b.ContactAdress,
                ContactEmail = b.ContactEmail,

            }).FirstOrDefaultAsync(b => b.GuestId == id);

            if (guestViewModel == null)
            {
                return NotFound();
            }

            return View(guestViewModel);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Guest guest)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        // GET: Guests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestId,ForeName,Surname")] Guest guest)
        {
            if (id != guest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.GuestId))
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
            return View(guest);
        }

        // GET: Guests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Guests == null)
            {
                return Problem("Entity set 'EventsDbContext.Guests'  is null.");
            }
            var guest = await _context.Guests.FindAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region manageGuest
        public async Task<IActionResult> Events(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            //Create the view model Staff with a list of events.
            GuestEventsVM vm = new GuestEventsVM();
            vm.events = GetGuestEvents(id);
            vm.guest = guest;

            //View is returned
            return View(vm);
        }

        public async Task<IActionResult> AddEvent(int? id)
        {
            //Find the guest with the correct ID
            Guest guest = _context.Guests.FirstOrDefault(m => m.GuestId == id);

            // Get their Events
            var GuestEvents = GetGuestEvents(id);
            // Get all the Events
            var AllEvents = _context.Events.ToList();
            //Only show the Events that they are not already signed up to e.g allEvents-Current Events
            var notGuestEvents = AllEvents.Except(GuestEvents);

            // Call the method to Create the view model for a list of Events that are bookable for that guest.
            GuestEventsListVM vm = AddEventsViewModel(guest, notGuestEvents);

            return View(vm);
        }


        //get the selection from the list and create the relationship between guest and staff
        [HttpPost]
        public IActionResult AddEvent(GuestEventsListVM vm)
        {
            if (vm.selectedEvent == "0")
            {
                return RedirectToAction("AddEvent", new { id = vm.guest.GuestId });
            }
            var selectedEvent = vm.selectedEvent;
            var guest = vm.guest;
            int EventId = Convert.ToInt32(selectedEvent);
            int guestId = Convert.ToInt32(guest.GuestId);
            GuestBooking guestBooking = new GuestBooking(EventId, guestId);
            try
            {
                _context.Add(guestBooking);
                _context.SaveChangesAsync();
                return RedirectToAction("Events", new { id = guestId });
            }
            catch
            {
                return RedirectToAction("AddEvent", new { id = vm.guest.GuestId });
            }

        }

        //Remove an event from a staff member.
        public async Task<IActionResult> Remove(int? eventId, int? guestId)
        {
            if (eventId == null || guestId == null || _context.GuestBookings == null)
            {
                return NotFound();
            }

            //get the staffing relationship that matches the guestID and eventID
            var guestBooking = _context.GuestBookings
                .FirstOrDefaultAsync(m => m.GuestID == guestId && m.EventID == eventId);
            if (guestBooking == null)
            {
                return NotFound();
            }
            //create a new view model
            EventGuestItemVM vm = new EventGuestItemVM();
            //populate the view model with the database model
            vm.guest = new Guest();
            vm.Event = new Event();
            //modify the view model to match the data of the relationship that needs to be deleted
            vm.guest = await _context.Guests.FindAsync(guestId);
            vm.Event = await _context.Events.FindAsync(eventId);

            //then return the "Remove" view with a prompt asking the user if they would like to remove this item
            return View(vm);
        }

        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(EventGuestItemVM vm)
        {
            if (_context.GuestBookings == null)
            {
                return Problem("Entity set 'guestBooking'  is null.");
            }
            var eventGuest = await _context.GuestBookings.FindAsync(vm.Event.EventId, vm.guest.GuestId);
            if (eventGuest != null)
            {
                _context.GuestBookings.Remove(eventGuest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Takes an array of values, each value will correspond to a guestID.
        //Each guest entity will then have its "Attended" value updated.
        [HttpPost]
        public async Task UpdateAttendance(int[] checkboxValues)
        {   
            foreach (int id in checkboxValues)
            {
                var guest = await _context.Guests.FindAsync(id);
                guest.Attended = true;

            }       
        }
        #endregion



        #region private methods
        private bool GuestExists(int id)
        {
          return _context.Guests.Any(e => e.GuestId == id);
        }

        //private method to get the events that staff member is involved with
        private List<Event> GetGuestEvents(int? id)
        {
            var Events = from Event in _context.Events
                         where Event.Guests.Any(c => c.GuestID == id)
                         select Event;

            List<Event> EventsList = Events.ToList();
            return EventsList;
        }

        //populate the list model given a Guest object and a lists of notGuestEvents
        private static GuestEventsListVM AddEventsViewModel(Guest guest, IEnumerable<Event> notGuestEvents)
        {
            // Build a ViewModel
            GuestEventsListVM vm = new GuestEventsListVM();
            List<SelectListItem> EventsList = new List<SelectListItem>();
            vm.Events = EventsList;
            vm.guest = guest;

            //Add prompt
            vm.Events.Add(new SelectListItem { Text = "--Select an Event--", Value = "0" });

            var selectItemList = notGuestEvents.Select(item => new SelectListItem
            { Text = item.EventTitle, Value = item.EventId.ToString() });
            vm.Events.AddRange(selectItemList);
            return vm;
        }
        #endregion
    }
}
