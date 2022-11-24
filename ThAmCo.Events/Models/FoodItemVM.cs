using ThAmCo.Catering.Data;

namespace ThAmCo.Events.Models
{
    public class FoodItemVM
    {
        public FoodItemVM() { }

        public int FoodItemId { get; set; }
        public string Title { get; set; }

        public bool isVegan { get; set; }

        public string Description { get; set; }
        public int Price { get; set; }

        public List<Menu> Menus { get; set; }
    }
}
