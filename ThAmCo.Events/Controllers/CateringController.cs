﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using ThAmCo.Catering.Data;
using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using ThAmCo.Events.ServiceLayer;

namespace ThAmCo.Events.Controllers
{
    public class CateringController : Controller
    {
        HttpClient client;

        public CateringController()
        {
            client = service.ServiceClient();
        }
        
        
        #region manage Food
        public async Task<ActionResult> FoodIndex()
        {
            IEnumerable<ServiceLayer.FoodItemDTO> food = await service.GetFoodItems(client);

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

        public async Task<ActionResult> FoodDetails(int id)
        {
            var DTO = await service.GetFoodItem(client, id);

            if (DTO == null)
            {
                return BadRequest();
            }

            var vm = new FoodItemVM();

            vm.FoodItemId = DTO.FoodItemId;
            vm.Title = DTO.Title;
            vm.isVegan = DTO.isVegan;
            vm.Description = DTO.Description;
            vm.Price = DTO.Price;
               
            return View(vm);
        }

        public async Task<ActionResult> EditFoodItem(int id)
        {
            FoodItemVM vm = new FoodItemVM();

            //Calls the method to get the FoodItem from the web service with the corresponding ID.
            var FoodItem = service.GetFoodItem(client, id);

            if (FoodItem == null)
            {
                return Problem("Entity set 'CateringDbContext.FoodItems'  is null.");
            }
            if (FoodItem != null)
            {
                //Set the foodItem view model attributes to that of the FoodItemDTO that is returned from the api.
                vm.FoodItemId = FoodItem.Result.FoodItemId;
                vm.Title = FoodItem.Result.Title;
                vm.Description = FoodItem.Result.Description;
                vm.isVegan = FoodItem.Result.isVegan;
                vm.Price = FoodItem.Result.Price;
            }
            
            return View(vm);
        }
        //working with POST but not PUT
        [HttpPost]
        public async Task<ActionResult> EditFoodItem(FoodItemVM vm)

        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7173");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            var food = service.VMfoodToDTOfood(vm);

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/FoodItems" +"/"+vm.FoodItemId.ToString(), food);
                return RedirectToAction("FoodIndex");
            }
            catch (Exception ex)
            {
                var message = ex.InnerException.Message.ToString();
                return View();
            }
            
        }

        public IActionResult CreateFood()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateFood(FoodItemVM vm)
        {
            try
            {
                //Call the method within the service layer to create a new food item
                await service.CreateFood(client, vm);
                
                return RedirectToAction("FoodIndex");
            }
            catch (Exception ex)
            {
                var message = ex.InnerException.Message.ToString();
                return View();
            }

            return View();
        }

        public async Task<ActionResult> DeleteFood(int id)
        {
            FoodItemVM vm = new FoodItemVM();

            //Calls the method to get the FoodItem from the web service with the corresponding ID.
            var FoodItem = service.GetFoodItem(client, id);

            if (FoodItem == null)
            {
                return Problem("Entity set 'CateringDbContext.FoodItems'  is null.");
            }
            if (FoodItem != null)
            {
                vm.FoodItemId = FoodItem.Result.FoodItemId;
                vm.Title = FoodItem.Result.Title;
                vm.Description = FoodItem.Result.Description;
                vm.isVegan = FoodItem.Result.isVegan;
            }

            return View(vm);
        }

        [HttpPost, ActionName("DeleteFood")]
        public async Task<ActionResult> DeleteFoodConfirmed(FoodItemVM vm)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7173");
            try
            {
                var deleteTask = client.DeleteAsync("api/FoodItems/" + vm.FoodItemId.ToString());
                return RedirectToAction("FoodIndex");

            }
            catch (Exception ex)
            {
                var message = ex.InnerException.Message.ToString();

            }
            return View();


        }
        #endregion

        #region manage Menus
        public async Task<ActionResult> MenuIndex()
        {
            IEnumerable<MenuDTO> menus = await service.GetMenus(client);

            if (menus == null)
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

        public IActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateMenu(MenuVM vm)
        {
            try
            {
                //Call the service layer method to send an API request to create a Menu
                await service.CreateMenu(client, vm);

                return RedirectToAction("MenuIndex");
            }
            catch (Exception ex)
            {
                var message = ex.InnerException.Message.ToString();
                return View();
            }

            return View();
        }

        public async Task<ActionResult> MenuDetails(int id)
        {
            //Get the appropriate menu from the API in DTO shape
            var DTO = await service.GetMenu(client, id);

            if (DTO == null)
            {
                return BadRequest();
            }

            //change the shape from DTO to ViewModel
            var vm = new MenuVM();

            vm.MenuId = DTO.MenuId;
            vm.MenuName = DTO.MenuName;
            vm.DateCreated = DTO.DateCreated;

            return View(vm);
        }

        public async Task<ActionResult> EditMenu(int id)
        {
            MenuVM vm = new MenuVM();

            //Calls the method to get the FoodItem from the web service with the corresponding ID.
            var menu = service.GetMenu(client, id);

            if (menu == null)
            {
                return Problem("Entity set 'CateringDbContext.Menu'  is null.");
            }
            if (menu != null)
            {
                //Set the foodItem view model attributes to that of the FoodItemDTO that is returned from the api.
                vm.MenuId = menu.Result.MenuId;
                vm.MenuName = menu.Result.MenuName;
                vm.DateCreated = menu.Result.DateCreated;
            }
            return View(vm);
        }

        //working with POST but not PUT
        [HttpPost]
        public async Task<ActionResult> EditMenu(MenuVM vm)

        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7173");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            var menu = service.VMmenuToDTOmenu(vm);

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Menus" + "/" + vm.MenuId.ToString(), menu);
                return RedirectToAction("MenuIndex");
            }
            catch (Exception ex)
            {
                var message = ex.InnerException.Message.ToString();
                return View();
            }

        }

        public async Task<ActionResult> DeleteMenu(int id)
        {
           MenuVM vm = new MenuVM();

            //Calls the api method to get the corresponding Menu, returns as a DTO
            var menu = service.GetMenu(client, id);

            if (menu == null)
            {
                return Problem("Entity set 'CateringDbContext.FoodItems'  is null.");
            }
            if (menu != null)
            {
                vm.MenuId = menu.Result.MenuId;
                vm.MenuName = menu.Result.MenuName;
                vm.DateCreated = menu.Result.DateCreated;
            }

            return View(vm);
        }

        [HttpPost, ActionName("DeleteMenu")]
        public async Task<ActionResult> DeleteMenuConfirmed(MenuVM vm)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7173");
            try
            {
                var deleteTask = client.DeleteAsync("api/Menus/" + vm.MenuId.ToString());
                return RedirectToAction("MenuIndex");

            }
            catch (Exception ex)
            {
                var message = ex.InnerException.Message.ToString();

            }

            return RedirectToAction("FoodIndex");

        }

        #endregion

        #region manage MenuFoodItems
        //public async Task<IActionResult> MenuFoodItems(int? id)
        //{
        //    //create the empty DTO.
        //    //need to create new method in service layer that will get menu with the foood items
        //    //MenuwithFoodItemDTO menu = await service.GetMenuFoodItems(client, id);

        //    if (menu == null)
        //    {
        //        return BadRequest();
        //    }

        //    //Create the empty view models
        //    MenuVM menuVM = new MenuVM();
        //    MenuFoodItemsVM menuFoodItemVM = new MenuFoodItemsVM();

        //    //Assign the Dto attributes to the menuVM attributes
        //    menuVM.MenuId = menu.menu.MenuId;
        //    menuVM.MenuName = menu.menu.MenuName;

        //    //Assign the foodItem DTO attributes to the FoodItemVM attributes
        //    var foodVM = menu.foodItems.Select(item => new FoodItemVM
        //    {
        //        FoodItemId = item.FoodItemId,
        //        Title = item.Title,
        //        isVegan = item.isVegan,
        //        Description = item.Description,
        //        Price = item.Price,


        //    }).ToList();
        //    //Compose the new view model that consists of both the menu and its food items
        //    menuFoodItemVM.menu = menuVM;
        //    menuFoodItemVM.FoodItems = foodVM;

        //    //View is returned
        //    return View(menuFoodItemVM);
        //}

        #endregion


    }
}
