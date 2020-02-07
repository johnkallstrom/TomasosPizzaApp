using Microsoft.AspNetCore.Mvc;

namespace TomasosPizzaApplication.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddItem(int id)
        {
            return View();
        }
    }
}