namespace ThAmCo.Events.ServiceLayer
{

    public class MenuwithFoodItemDTO
    {
        public MenuwithFoodItemDTO() { }
        public MenuDTO menu { get; set; }

        public ICollection<FoodItemDTO> foodItems { get; set; }
    }
}
