using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
        //validation? 
        [MinLength(3), MaxLength(3)]
        [Key]
        public int FoodItemId{ get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        //need to take a look at this, why use float?
        public float UnitPrice { get; set; }

        public List<MenuFoodItem> Menus { get; set; }
    }
}
