using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class GuestViewModel
    {
        
        public int GuestId { get; set; }

        [Required(ErrorMessage = "You must provide a first name")]
        [MaxLength(25)]
        [Display(Name = "First Name")]
        public string Forename { get; set; }

        [Required(ErrorMessage = "You must provide a last name")]
        [MaxLength(25)]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Adress")]
        public string ContactAdress { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email Address")]
        public string ContactEmail { get; set; }

        public List<GuestBooking> Events { get; set; }
    }
}
