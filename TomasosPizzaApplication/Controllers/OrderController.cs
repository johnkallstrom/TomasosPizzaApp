using Microsoft.AspNetCore.Mvc;

namespace TomasosPizzaApplication.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult ViewProducts()
        {
            return View();
        }
    }
}