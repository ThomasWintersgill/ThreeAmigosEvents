using System.ComponentModel;

namespace ThAmCo.Catering.DTOs
{
    public class FoodItemDTO
    {
        public FoodItemDTO() { }

        public int FoodItemId { get; set; }
        public string Title { get; set; }

        public bool isVegan     { get; set; }

        public string Description { get; set; }
        public int Price { get; set; }
    }
}
