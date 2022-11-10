using System.ComponentModel.DataAnnotations;
using ThAmCo.Events.Data;
namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        [MinLength(3), MaxLength(3)]
        [Key]
        public int FoodBookingId { get; set; }

        public int ClientReferenceId { get; set; }

        public int NumberOfGuests { get; set; }

        
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public EventId EventId { get; set; }
        public Event Event { get; set; }
    }
}
