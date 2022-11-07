namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        public MenuFoodItem(int menuId, int foodItemId)
        {
            MenuId = menuId;
            FoodItemId = foodItemId;
        }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }


    }
}
