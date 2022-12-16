#nullable disable

using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public Staff(int staffId, string forename, string surname, string payRollNumber, string contactNumber, string adress)
        {
            StaffId = staffId;
            Forename = forename;
            Surname = surname;
            this.payRollNumber = payRollNumber;
            ContactNumber = contactNumber;
            Adress = adress;
        }

        public Staff()
        {
        }

        public int StaffId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Forename { get; set; }

        [Required]
        [MaxLength(25)]
        public string Surname { get; set; }

        [Required]
        public string payRollNumber { get; set; }

        //[Required]
        //public Department department { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string Adress { get; set; }

        public string ContactEmail { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public List<Staffing> Events { get; set; }
    }
}
