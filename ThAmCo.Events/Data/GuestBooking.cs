#nullable disable

namespace ThAmCo.Events.Data
{
    public class GuestBooking


    {
        public GuestBooking()
        {
        }

        public GuestBooking(int eventID, int guestID)
        {
            EventID = eventID;
            GuestID = guestID;
        }

        public int EventID { get; set; }
        public Event Event { get; set; }

        public int GuestID  { get; set; }
        public Guest Guest { get; set; }



    }
}
