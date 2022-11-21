using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class GuestEventsListVM
    {
        public GuestEventsListVM()
        {
        }

        public string selectedEvent { get; set; }
        public Guest guest { get; set; }
        public List<SelectListItem> Events { get; set; }

    }

    public class GuestEventsVM
    {
        public GuestEventsVM() { }

        public Guest guest { get; set; }
        public List<Event> events { get; set; }
    }

    public class EventGuestItemVM
    {
        public EventGuestItemVM()
        { }

        public Guest guest { get; set; }
        public Event Event { get; set; }
    }
}
