#nullable disable

namespace ThAmCo.Events.Data
{
    public class GuestBooking
    {
        public int EventID { get; set; }
        public Event Event { get; set; }

        public int GuestID  { get; set; }
        public Guest Guest { get; set; }



    }
}
