using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class StaffEventsListVM
    {
        public StaffEventsListVM()
        {
        }

        public string selectedEvent { get; set; }
        public Staff staff { get; set; }
        public List<SelectListItem> Events { get; set; }

    }

    public class StaffEventsVM
    {
        public StaffEventsVM() { }

        public Staff staff { get; set; }
        public List<Event> events { get; set; }
    }

    public class EventStaffItemVM
    {
        public EventStaffItemVM()
        { }

        public Staff staff { get; set; }
        public Event Event { get; set; }
    }

    public class EventsStaffVM
    {
        public EventsStaffVM()
        {
        }

        public Event Event { get; set; }

        public List<Staff> staff { get; set; }
    }

    public class EventStaffListVM
    {
        public EventStaffListVM()
        {
        }

        public string selectedStaff { get; set; }
        public Event Event { get; set; }
        public List<SelectListItem> Staff { get; set; }

    }
}
