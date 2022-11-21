using ThAmCo.Catering.Data;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class MenuVM
    {
        public MenuVM()
        {
        }

        public int MenuId { get; set; }

        public string MenuName  { get; set; }

        public List<FoodItem> FoodItems { get; set; }
    }

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

    public class MenuFoodItemsVM
    {
        public MenuFoodItemsVM() { }
        public MenuVM menu { get; set; }

        public ICollection<FoodItemVM> FoodItems { get; set; }
    }
}
