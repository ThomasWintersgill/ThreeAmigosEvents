#nullable disable

using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Guest
    {
        public Guest()
        {
        }

        public Guest(int guestId, string forename, string surname, string contactNumber, string contactAdress, string contactEmail)
        {
            GuestId = guestId;
            Forename = forename;
            Surname = surname;
            ContactNumber = contactNumber;
            ContactAdress = contactAdress;
            ContactEmail = contactEmail;
        }

        [MinLength(3), MaxLength(3)]
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
