using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        
     
    }
}
