using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;


namespace ThAmCo.Events.ServiceLayer
{
    public class Service
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
        public static async Task<IEnumerable<MenuDTO>> GetMenu(HttpClient client)
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

        public static async Task<ActionResult> GetMenuFoodItems(HttpClient client, int id)
        {
            MenuwithFoodItemDTO menuFoodItems = new MenuwithFoodItemDTO();
            string url = "api/Menus" + id.ToString();

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                menuFoodItems = await response.Content.ReadAsAsync<MenuwithFoodItemDTO>;
            }
            else
            {
                Debug.WriteLine("Index received a bad response");
            }

            return menuFoodItems;
        }

        public static async Task<IEnumerable<FoodItemDTO>> GetFoodItem(HttpClient client, int id)
        {
            IEnumerable<FoodItemDTO> menuWithFood = new List<FoodItemDTO>();
            string url = "api/Menus" +id.ToString();

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                menuWithFood = await response.Content.ReadAsAsync<IEnumerable<FoodItemDTO>>();
            }
            else
            {
                Debug.WriteLine("Index received a bad response");
            }

            return menuWithFood;

        }




    }
}
