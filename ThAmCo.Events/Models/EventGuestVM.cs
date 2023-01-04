using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{

    public class EventGuestListVM
    {
        public EventGuestListVM()
        {
        }

        public string selectedGuest { get; set; }
        public Event events { get; set; }
        public List<SelectListItem> Guests { get; set; }

    }

    public class EventGuestVM
    {
        public EventGuestVM()
        {
        }

        public Event Event { get; set; }

        public List<Guest> guests { get; set; }
    }





}
