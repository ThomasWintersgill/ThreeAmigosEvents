using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ThAmCo.Catering.Data;

namespace ThAmCo.Events.Models
{
    public class FoodItemVM
    {
        public FoodItemVM() { }

            public int FoodItemId { get; set; }

            [Required(ErrorMessage = "You must provied a title")]
            [MaxLength(40)]
            [Display(Name = "Food Title")]
            public string Title { get; set; }

            [Display(Name = "Tick for Vegan")]
            public bool isVegan { get; set; }

            [Required(ErrorMessage = "You must provied a description")]
            [MaxLength(100)]
            public string Description { get; set; }
            public int Price { get; set; }

        //public List<Menu> Menus { get; set; }
    }
}
