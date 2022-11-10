namespace ThAmCo.Events.Data
{
    public class Staffing
    {
        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
