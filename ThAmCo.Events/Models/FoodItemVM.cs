using System.ComponentModel.DataAnnotations;
using ThAmCo.Catering.Data;
using ThAmCo.Events.Data;


namespace ThAmCo.Events.Models
{
    public class FoodItemDTO
    {
        public FoodItemDTO() { }

        public int FoodItemId { get; set; }
        public string Title { get; set; }

        public bool isVegan { get; set; }

        public string Description { get; set; }
        public int Price { get; set; }

        public List<Menu> Menus { get; set; }
    }
}
