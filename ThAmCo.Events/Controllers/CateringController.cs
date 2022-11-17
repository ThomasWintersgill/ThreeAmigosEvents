using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult> MenuDetails(int id)
        {
            IEnumerable<MenuwithFoodItemDTO> menu = await Service.GetMenuFoodItems(client);

            if (menu == null)
            {
                return BadRequest();
            }
            var vm = menu.Select(item => new MenuVM
            {
                MenuId = item.menu
                
            }).ToList();



            return View(vm);
        }




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
