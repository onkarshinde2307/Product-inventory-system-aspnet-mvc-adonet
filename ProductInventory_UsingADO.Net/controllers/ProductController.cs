using System.Web.Mvc;
using ProductInventory_ADO.Models;

namespace ProductInventory_ADO.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository repo = new ProductRepository();

        public ActionResult Index()
        {
            var products = repo.GetAll();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = repo.GetById(id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var product = repo.GetById(id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = repo.GetById(id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
