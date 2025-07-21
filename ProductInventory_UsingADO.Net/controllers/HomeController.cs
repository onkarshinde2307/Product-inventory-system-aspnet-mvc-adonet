using System.Linq;
using System.Web.Mvc;
using ProductInventory_ADO.Models;

namespace ProductInventory_ADO.Controllers
{
    public class HomeController : Controller
    {
        private ProductRepository repo = new ProductRepository();

        public ActionResult Index()
        { 
            return View( );
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
