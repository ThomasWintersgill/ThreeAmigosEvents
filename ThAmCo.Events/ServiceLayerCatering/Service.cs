using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using ThAmCo.Catering.Data;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.ServiceLayer
{
    public class service
    {
        #region HTTPClient
        public static HttpClient ServiceClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7173/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }
        #endregion

        //is there any point in this? or should i just have the one method that gets both the menu and the food items together and only
        //displays the appropriate information
        public static async Task<IEnumerable<MenuDTO>> GetMenus(HttpClient client)
        {
            //create a list of menuDTO
            IEnumerable<MenuDTO> menu = new List<MenuDTO>();

            HttpResponseMessage response = await client.GetAsync("api/Menus");
            if (response.IsSuccessStatusCode)
            {
                //populate the client side dto with the dtos from the api
                menu = await response.Content.ReadAsAsync<IEnumerable<MenuDTO>>();
            }
            else
            {
                Debug.WriteLine("Index received a bad response");
            }

            return menu;
        }

        public static async Task<MenuDTO> GetMenu(HttpClient client, int id)
        {
            MenuDTO menu = new MenuDTO();
            HttpResponseMessage response = await client.GetAsync("api/Menus/" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                menu = await response.Content.ReadAsAsync<MenuDTO>();
            }
            else
            {
                Debug.WriteLine("Index received a bad response");
            }

            return menu;
        }
        public static async Task<MenuDTO> GetMenu(HttpClient client, int? id)
        {
            MenuDTO DTO = new MenuDTO();
            string url = "api/Menus/" + id.ToString();

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                DTO = await response.Content.ReadAsAsync<MenuDTO>();
            }
            else
            {
                Debug.WriteLine("Index received a bad response");
            }

            return DTO;
        }
        public static async Task<FoodItemDTO>GetFoodItem(HttpClient client, int id)
        {
            FoodItemDTO FoodItem = new FoodItemDTO();
            HttpResponseMessage response = await client.GetAsync("api/FoodItems/" +id.ToString());

            if (response.IsSuccessStatusCode)
            {
                FoodItem = await response.Content.ReadAsAsync<FoodItemDTO>();
            }
            else
            {
                Debug.WriteLine("Index received a bad response");
            }

            return FoodItem;
        }
        public static async Task<IEnumerable<FoodItemDTO>> GetFoodItems(HttpClient client)
        {
            IEnumerable<FoodItemDTO> Fooditem = new List<FoodItemDTO>();
            HttpResponseMessage response = await client.GetAsync("api/FoodItems");
            if (response.IsSuccessStatusCode)
            {
                Fooditem = await response.Content.ReadAsAsync<IEnumerable<FoodItemDTO>>();
            }
            else
            {
                Debug.WriteLine("Index received a bad response");
            }

            return Fooditem;

        }

        public static async Task CreateFood(HttpClient client, FoodItemVM vm)
        {
            //Transform the Food VM to a Food DTO.
            var food = VMfoodToDTOfood(vm);

            //Send a post request with the food in the DTO shape.
            HttpResponseMessage response = await client.PostAsJsonAsync("api/FoodItems", food);

        }

        public static async Task CreateMenu(HttpClient client, MenuVM vm)
        {
            //Transform the Menu VM to a Menu DTO.
            var menu = VMmenuToDTOmenu(vm);

            //Send a post request with the food in the DTO shape.
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Menus", menu);

        }

        //Takes a foodItem View Mdoel and returns a foodItemDTO
        public static FoodItemDTO VMfoodToDTOfood(FoodItemVM foodVM)
        {

            var food = new FoodItemDTO();
            food.FoodItemId = foodVM.FoodItemId;
            food.Title = foodVM.Title;
            food.Description = foodVM.Description;
            food.isVegan = foodVM.isVegan;
            food.Price = foodVM.Price;

            return food;

        }

        //Takes a viewmodel menu and returns a DTO menu
        public static MenuDTO VMmenuToDTOmenu(MenuVM menuVM)
        {
            var menu = new MenuDTO();
            menu.MenuId = menuVM.MenuId;
            menu.MenuName = menuVM.MenuName;
            menu.DateCreated = menu.DateCreated;

            return menu;
        }



    }
}
