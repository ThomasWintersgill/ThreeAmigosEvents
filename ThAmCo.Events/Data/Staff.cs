#nullable disable

namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public int StaffId { get; set; }

        public String StaffName { get; set; }

        public List<Staffing> Events { get; set; }
    }
}
