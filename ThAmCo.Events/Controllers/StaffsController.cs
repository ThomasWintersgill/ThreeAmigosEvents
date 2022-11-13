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
    public class StaffsController : Controller
    {
        private readonly EventsDbContext _context;

        public StaffsController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
              return View(await _context.Staff.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,Forename,Surname")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,Forename,Surname")] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffId))
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
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staff == null)
            {
                return Problem("Entity set 'EventsDbContext.Staff'  is null.");
            }
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
          return _context.Staff.Any(e => e.StaffId == id);
        }

        public async Task<IActionResult> Events(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            StaffEventsVM vm = new StaffEventsVM();
            vm.events = GetStaffEvents(id);
            vm.staff = staff;

            return View(vm);
        }

        // GET: Staffs/Create
        public async Task<IActionResult> AddEvent(int? id)
        {
            // who wants the workshop
            Staff staff = _context.Staff.FirstOrDefault(m => m.StaffId == id);

            /// Get a list of workshops they have yet to associate with
            // Get their Events
            var StaffEvents = GetStaffEvents(id);
            // Get all the Events
            var AllEvents = _context.Events.ToList();
            //Only show the Events that they are not already signed up to e.g allEvents-Current Events
            var notStaffEvents = AllEvents.Except(StaffEvents);

            /// Create and Populate a VM with a Selectable List

            StaffEventsListVM vm = AddEventsViewModel(staff, notStaffEvents);

            return View(vm);
        }

        //need to be looking in here to fix the issue with relationship not being created
        [HttpPost]
        public IActionResult AddEvent(StaffEventsListVM vm)
        {
            if (vm.selectedEvent== "0")
            {
                return RedirectToAction("AddEvent", new { id = vm.staff.StaffId });
            }
            var selectedEvent = vm.selectedEvent;
            var staff = vm.staff;
            int EventId = Convert.ToInt32(selectedEvent);
            int staffId = Convert.ToInt32(staff.StaffId);
            Staffing staffing = new Staffing (EventId, staffId);
            try
            {
                _context.Add(staffing);
                _context.SaveChangesAsync();
                return RedirectToAction("Events", new { id = staffId });
            }
            catch
            {
                return RedirectToAction("AddEvent", new { id = vm.staff.StaffId });
            }

        }
        //private method to get the events that staff member is involved with
        private List<Event> GetStaffEvents(int? id)
        {
            var Events = from Event in _context.Events
                         where Event.Staff.Any(c => c.StaffId == id)
                         select Event;

            List<Event> EventsList = Events.ToList();
            return EventsList;
        }

        private static StaffEventsListVM AddEventsViewModel(Staff staff, IEnumerable<Event> notStaffEvents)
        {
            // Build a ViewModel
            StaffEventsListVM vm = new StaffEventsListVM();
            List<SelectListItem> EventsList = new List<SelectListItem>();
            vm.Events = EventsList;
            vm.staff = staff;

            // Add a Row at the top as a promt
            vm.Events.Add(new SelectListItem { Text = "--Select an Event--", Value = "0" });

            // Add a Row for each of the Worksheets to be selectable
            //foreach (WorkshopModel.Workshop item in notMyWorkshops)
            //{
            //    vm.Workshops.Add(new SelectListItem { Text = item.Name, Value = item.WorkshopId.ToString() });
            //}
            var selectItemList = notStaffEvents.Select(item => new SelectListItem
            { Text = item.EventTitle, Value = item.EventId.ToString() });
            vm.Events.AddRange(selectItemList);
            return vm;
        }

    }
}
