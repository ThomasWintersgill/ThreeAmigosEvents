namespace ThAmCo.Catering.DTOs
{
   

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
