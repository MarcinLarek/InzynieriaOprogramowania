using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjektZaliczeniowy.Controllers
{
    public class PostViewModelController : Controller
    {
        // GET: PostViewModelController
        public ActionResult Index()
        {
            // return View();
            return RedirectToAction("Index", "Home");
        }

        // GET: PostViewModelController/Details/5
        public ActionResult Details(int id)
        {
            //return View();
            return RedirectToAction("Index", "Home");
        }

        // GET: PostViewModelController/Create
        public ActionResult Create()
        {
            //return View();
            return RedirectToAction("Index", "Home");
        }

        // POST: PostViewModelController/Create
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

        // GET: PostViewModelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostViewModelController/Edit/5
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

        // GET: PostViewModelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostViewModelController/Delete/5
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
