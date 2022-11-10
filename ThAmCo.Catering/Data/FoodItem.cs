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
        public int FoodItemId{ get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        //pence stored as integer
        public int UnitPrice { get; set; }

        public List<MenuFoodItem> Menus { get; set; }
    }
}
