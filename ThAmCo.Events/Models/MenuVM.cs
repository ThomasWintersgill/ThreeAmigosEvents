﻿using ThAmCo.Catering.Data;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Models
{
    public class MenuVM
    {
        public MenuVM()
        {
        }

        public int MenuId { get; set; }

        public string MenuName  { get; set; }

        public List<FoodItem> FoodItems { get; set; }
    }
}