namespace ThAmCo.Catering.DTOs
{
    public class FoodItemsDTO
    {
        public FoodItemsDTO() { }

        public int FoodItemId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }

    public class MenuDTO
    {
        public MenuDTO() { }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
    }

    public class MenuwithFoodItemDTO
    {
       public MenuwithFoodItemDTO() { }
       public MenuDTO menu { get; set; }

      public ICollection<FoodItemsDTO> FoodItems { get; set; }
    }
}
