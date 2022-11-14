#nullable disable

namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public int StaffId { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public List<Staffing> Events { get; set; }
    }
}
