using System.ComponentModel.DataAnnotations;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class GuestViewModel
    {
        
        public int GuestId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Forename { get; set; }

        [Required]
        [MaxLength(25)]
        public string Surname { get; set; }

        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContactAdress { get; set; }

        public string ContactEmail { get; set; }

        public List<GuestBooking> Events { get; set; }
    }
}
