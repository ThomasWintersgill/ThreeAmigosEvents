using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
        public FoodItem()
        {
        }

        public FoodItem(int foodItemId, string title, FoodCategory category, string description, int unitPrice, bool isVegan, DateTime dateCreated)
        {
            FoodItemId = foodItemId;
            Title = title;
            Category = category;
            Description = description;
            UnitPrice = unitPrice;
            IsVegan = isVegan;
            DateCreated = dateCreated;
        }

        [MinLength(3), MaxLength(3)]
        public int FoodItemId{ get; set; }

        [Required]
        [MaxLength(50)]
        public string Title{ get; set; }

        public FoodCategory Category { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        //pence stored as integer
        [Range (1, 10000)]
        public int UnitPrice { get; set; }

        [Required]
        public bool IsVegan { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public List<MenuFoodItem> Menus { get; set; }
    }
}
