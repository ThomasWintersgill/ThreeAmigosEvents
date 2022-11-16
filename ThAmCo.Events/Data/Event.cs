#nullable disable

using System.ComponentModel.DataAnnotations;
using ThAmCo.Catering.Data;

namespace ThAmCo.Events.Data
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [MaxLength(50)]
        public string EventTitle { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime EventTime { get; set; }

        [Required]
        public bool HasFirstAider { get; set; }

        [Required]
        //foreign key into the venues data model
        public string EventType { get; set; }

        public List<GuestBooking> Guests { get; set; }

        public List<Staffing> Staff { get; set; }



    }
}
