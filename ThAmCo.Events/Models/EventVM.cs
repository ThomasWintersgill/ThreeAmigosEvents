using System.ComponentModel.DataAnnotations;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class EventVM
    {

        public int EventId { get; set; }

        [Required(ErrorMessage = "You must provied an Event Title")]
        [MaxLength(50)]
        [Display(Name = "Event Title")]
        public string EventTitle { get; set; }

        [Required(ErrorMessage = "You must provied an Event Date")]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "You must provied an Event Time")]
        [Display(Name = "Event Time")]
        [DataType(DataType.Time)]
        public DateTime EventTime { get; set; }

        
        [Display(Name = "Tick box if this Event has a first aider assigned")]
        public bool HasFirstAider { get; set; }

        [Display(Name = "Event Type")]
        [Required]
        //foreign key into the venues data model
        public string EventType { get; set; }

       
    }
}
