using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Event
    {
        public int EventId { get; set; }

        public string EventTitle { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime EventTime { get; set; }

        
        //foreign key into the venues data model
        public string EventType { get; set; }

        public Foodbooking FoodBooking
        public List<Guest> Guests { get; set; }

        public List<Staff> Staffs { get; set; }



    }
}
