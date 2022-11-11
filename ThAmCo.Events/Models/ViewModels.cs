using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class ViewModels
    {
        public class StaffEventsVm
        {
            public StaffEventsVm()
            {
            }

            public Staff staff { get; set; }


            public List<Event> Events { get; set; }
        }
    }
}
