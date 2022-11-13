#nullable disable

namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public int StaffId { get; set; }

        public String Forename{ get; set; }

        public String Surname { get; set; }

        public List<Staffing> Events { get; set; }
    }
}
