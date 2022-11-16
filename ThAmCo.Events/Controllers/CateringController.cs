using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThAmCo.Events.Controllers
{
    public class CateringController : Controller
    {
        HttpClient client;

        public CateringController()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:7090/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        // GET: CateringController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CateringController/Details/5
        public ActionResult Details(int id)
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
