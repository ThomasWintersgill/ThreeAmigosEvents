using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.DTOs;


namespace ThAmCo.Events.Controllers
{
    public class EventsController : Controller
    {
        #region dbContextInjection
        private readonly EventsDbContext _context;

        public EventsController(EventsDbContext context)
        {
            _context = context;
        }
        #endregion

        #region CRUD
        // GET: Events
        public async Task<IActionResult> Index()
        {
            //Type projection from Model into ViewModel.
            var vm = _context.Events.Select(item => new EventVM
            {
                EventId = item.EventId,
                EventDate = item.EventDate,
                EventTime = item.EventTime,
                EventTitle = item.EventTitle,
                EventType = item.EventType,

            }).ToList();

            return View(vm);
        }


        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public async Task<IActionResult> Create()
        {
            var eventList = await GetEventTypes();

            ViewData["EventType"] = new SelectList(eventList, "Id", "Title");

            return View();
        }

        

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //Fill Event type box incase valdiation fails.
            var eventList = await GetEventTypes();

            ViewData["EventType"] = new SelectList(eventList, "Id", "Title");

            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }


        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventTitle,EventDate,EventTime,EventType")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'EventsDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region manage EventStaff
        public async Task<IActionResult> Staff(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventt = await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);
            if (eventt == null)
            {
                return NotFound();
            }

            //Create the view model Staff with a list of events.
            EventsStaffVM vm = new EventsStaffVM();
            vm.staff = GetEventsStaff(id);
            vm.Event = eventt;

            //View is returned
            return View(vm);
        }

        // GET: Staffs/Create
        public async Task<IActionResult> AddStaff(int? id)
        {
            //Find the staff member with the correct ID
            Event Event = _context.Events.FirstOrDefault(m => m.EventId == id);

            // Get their Events
            var EventStaff = GetEventsStaff(id);
            // Get all the Events
            var AllStaff = _context.Staff.ToList();
            //Only show the staff that are not already assigned to this event
            var notEventStaff = AllStaff.Except(EventStaff);

            //Create the view model for a list of Events that are bookable for that staff member.
            EventStaffListVM vm = AddStaffViewModel(Event, notEventStaff);

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddStaff(EventStaffListVM vm)
        {
            if (vm.selectedStaff == "0")
            {
                return RedirectToAction("AddStaff", new { id = vm.Event.EventId});
            }
            var selectedStaff = vm.selectedStaff;
            var Event = vm.Event;
            int staffId = Convert.ToInt32(selectedStaff);
            int EventId = Convert.ToInt32(Event.EventId);
            Staffing staffing = new Staffing(staffId, EventId);
            try
            {
                _context.Add(staffing);
                _context.SaveChangesAsync();
                return RedirectToAction("Staff", new { id = EventId });
            }
            catch
            {
                return RedirectToAction("AddStaff", new { id = vm.Event.EventId });
            }
        }

        //Remove a staff member from an event
        public async Task<IActionResult> Remove(int? eventId, int? staffId)
        {
            if (eventId == null || staffId == null || _context.staffing == null)
            {
                return NotFound();
            }

            //get the staffing relationship that matches the staffID and eventID
            var staffing = _context.staffing
                .FirstOrDefaultAsync(m => m.StaffId == staffId && m.EventId == eventId);
            if (staffing == null)
            {
                return NotFound();
            }

            //create a new view model
            EventStaffItemVM vm = new EventStaffItemVM();
            //populate the view model with the database model
            vm.staff = new Staff();
            vm.Event = new Event();
            //modify the view model to match the data of the relationship that needs to be deleted
            vm.staff = await _context.Staff.FindAsync(staffId);
            vm.Event = await _context.Events.FindAsync(eventId);

            //then return the "Remove" view with a prompt asking the user if they would like to remove this item
            return View(vm);
        }


        //Takes the view model from above to delete a relationship
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(EventStaffItemVM vm)
        {
            if (_context.staffing == null)
            {
                return Problem("Entity set 'staffing'  is null.");
            }
            var workshopStaff = await _context.staffing.FindAsync(vm.staff.StaffId, vm.Event.EventId);
            if (workshopStaff != null)
            {
                _context.staffing.Remove(workshopStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region manage EventGuests

        public async Task<IActionResult> Guests(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventt = await _context.Events.FirstOrDefaultAsync(m => m.EventId == id);
            if (eventt == null)
            {
                return NotFound();
            }

            //Create the view model Staff with a list of events.
            EventGuestVM vm = new EventGuestVM();
            vm.guests = GetEventsGuest(id);
            vm.Event = eventt;

            //View is returned
            return View(vm);
        }
        #endregion

        #region private methods
        private bool EventExists(int id)
        {
          return _context.Events.Any(e => e.EventId == id);
        }

        //Takes an Event Id and Returns a list of Staff associated with that particular Event.
        private List<Staff> GetEventsStaff(int? id)
        {
            var Staff = from Staffs in _context.Staff
                         where Staffs.Events.Any(c => c.EventId == id)
                         select Staffs;

            List<Staff> StaffList = Staff.ToList();
            return StaffList;
        }

        //Takes an Event Id and Returns a list of Guest associated with that particular Event.
        private List<Guest> GetEventsGuest(int? id)
        {
            var guest = from Guest in _context.Guests
                        where Guest.Events.Any(c => c.EventID == id)
                        select Guest;

            List<Guest> GuestList = guest.ToList();
            return GuestList;
        }

        private static EventStaffListVM AddStaffViewModel(Event Event, IEnumerable<Staff> notEventStaff)
        {
            // Build a ViewModel
            EventStaffListVM vm = new EventStaffListVM();
            List<SelectListItem> StaffList = new List<SelectListItem>();
            vm.Staff = StaffList;
            vm.Event = Event;

            //Add prompt
            vm.Staff.Add(new SelectListItem { Text = "--Select a Staff Member--", Value = "0" });

            var selectItemList = notEventStaff.Select(item => new SelectListItem
            { Text = item.Forename + " " + item.Surname, Value = item.StaffId.ToString() });
            vm.Staff.AddRange(selectItemList);
            return vm;
        }

        // Call web service and get a list of categories
        private async Task<List<EventTypeDTO>> GetEventTypes()
        {
            var eventTypes = new List<EventTypeDTO>().AsEnumerable();

            // Create and initial Http Client
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7088/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            // Call web service
            HttpResponseMessage response = await client.GetAsync("api/EventTypes");
            if (response.IsSuccessStatusCode)
            {
                // Decode response into a DTO
                eventTypes = await response.Content.ReadAsAsync<IEnumerable<EventTypeDTO>>();
            }
            else
            {
                throw new ApplicationException("Something went wrong calling the API:" +
                             response.ReasonPhrase);
            }

            return eventTypes.ToList();
        }
        #endregion
    }
}
