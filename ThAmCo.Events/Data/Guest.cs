#nullable disable

namespace ThAmCo.Events.Data
{
    public class Guest
    {
        public int GuestId { get; set; }

        public string ForeName { get; set; }

        public string Surname { get; set; }

        public List<GuestBooking> Events { get; set; }
    }
}
