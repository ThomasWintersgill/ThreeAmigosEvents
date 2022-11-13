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


}
