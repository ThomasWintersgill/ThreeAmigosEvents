using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ThAmCo.Events.Models;
using ThAmCo.Events.ServiceLayer;

namespace ThAmCo.Events.Controllers
{
    public class CateringController : Controller
    {
        HttpClient client;

        public CateringController()
        {
            client = Service.ServiceClient();
        }
        
        public async Task<ActionResult> MenuIndex()
        {
            IEnumerable<MenuDTO> menus = await Service.GetMenu(client);

            if ( menus == null)
            {
                return BadRequest();
            }

            var vm = menus.Select(item => new MenuVM
            {
                MenuId = item.MenuId,
                MenuName = item.MenuName
            }).ToList();

            return View(vm);
        }
   
        public async Task<ActionResult> FoodIndex()
        {
            IEnumerable<ServiceLayer.FoodItemDTO> food = await Service.GetFoodItem(client);

            if (food == null)
            {
                return BadRequest();
            }

            var vm = food.Select(item => new FoodItemVM
            {
                FoodItemId = item.FoodItemId,
                Title = item.Title,
                isVegan = item.isVegan,
                Description = item.Description,
                Price = item.Price,

                
            }).ToList();

            return View(vm);
        }

        //Get all the events that this staff member is booked onto.
        //The name of this method is the associated VIEW --"Events".
        public async Task<IActionResult> MenuFoodItems(int? id)
        {
            //create the empty DTO.
            MenuwithFoodItemDTO menu = await Service.GetMenuFoodItems(client, id);

            if (menu == null)
            {
                return BadRequest();
            }

            //Create the empty view models
            MenuVM menuVM = new MenuVM();
            MenuFoodItemsVM menuFoodItemVM = new MenuFoodItemsVM();
          
            //Assign the Dto attributes to the menuVM attributes
            menuVM.MenuId = menu.menu.MenuId;
            menuVM.MenuName = menu.menu.MenuName;

            //Assign the foodItem DTO attributes to the FoodItemVM attributes
            var foodVM = menu.FoodItems.Select(item => new FoodItemVM
            {
                FoodItemId = item.FoodItemId,
                Title = item.Title,
                isVegan = item.isVegan,
                Description = item.Description,
                Price = item.Price,


            }).ToList();
            //Compose the new view model that consists of both the menu and its food items
            menuFoodItemVM.menu = menuVM;
            menuFoodItemVM.FoodItems = foodVM;

            //View is returned
            return View(menuFoodItemVM);
        }



            //    return View(vm);
            //}

            //public async Task<ActionResult> MenuDetails(int id)
            //{
            //    // "{id:int}/Workshop/"
            //    string url = "api/Menus/" + id.ToString();
            //    MenuwithFoodItemDTO menuFoodItems= new MenuwithFoodItemDTO();
            //    HttpResponseMessage response = await client.GetAsync(url);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string resulString = await response.Content.ReadAsStringAsync();
            //        menuFoodItems = JsonConvert.DeserializeObject<MenuwithFoodItemDTO>(resulString);
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }


            //    var myActualData = menuFoodItems;
            //    MenuVM vm = new MenuVM();

            //    vm.staff = new Models.StaffVM
            //    {
            //        Name = myActualData.staff.Name,
            //    };


            //    vm.workshop = myActualData.workshop.Select(item => new Models.WorkshopVM
            //    {
            //        Name = item.Name,
            //        DateTime = item.DateAndTime
            //    }).ToList();


            //    return View(vm);
            //}




            // GET: CateringController
            public ActionResult Index()
        {
            return View();
        }

        // GET: CateringController/Details/5
        public ActionResult MenuDetails(int id)
        {
            return View();
        }

        // GET: CateringController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CateringController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CateringController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CateringController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CateringController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CateringController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
