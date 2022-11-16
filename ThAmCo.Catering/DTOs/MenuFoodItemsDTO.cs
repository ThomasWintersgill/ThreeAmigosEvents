namespace ThAmCo.Catering.DTOs
{
   
    public class MenuwithFoodItemDTO
    {
       public MenuwithFoodItemDTO() { }
       public MenuDTO menu { get; set; }

      public ICollection<FoodItemDTO> FoodItems { get; set; }
    }
}
