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

        public static async Task<IEnumerable<MenuDTO>>GetMenu(HttpClient client)
        {
            IEnumerable<MenuDTO> menu = new List<MenuDTO>();

            HttpResponseMessage response = await client.GetAsync("api/Menus");
            if (response.IsSuccessStatusCode)
            {
                menu = await response.Content.ReadAsAsync<IEnumerable<MenuDTO>>();
            }
            else
            {
                Debug.WriteLine("Index recieved a bad response");
            }

            return menu;
        }
    }
}
