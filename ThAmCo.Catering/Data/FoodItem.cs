using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
        public FoodItem(int foodItemId, string description, int unitPrice)
        {
            FoodItemId = foodItemId;
            Description = description;
            UnitPrice = unitPrice;
        }


        //validation? 
        
        
        public int FoodItemId{ get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        //need to take a look at this, why use float?
        public int UnitPrice { get; set; }

        public List<MenuFoodItem> Menus { get; set; }
    }
}
