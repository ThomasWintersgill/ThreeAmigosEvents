using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        public Menu(int menuId, string menuName, DateTime dateCreated)
        {
            MenuId = menuId;
            MenuName = menuName;
            DateCreated = dateCreated;
        }

        [MinLength(3), MaxLength(3)]
        [Key]
        public int MenuId { get; set; }

        [Required]
        [MaxLength(50)]
        public string MenuName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public List<MenuFoodItem> FoodItems { get; set; }
    }
}

