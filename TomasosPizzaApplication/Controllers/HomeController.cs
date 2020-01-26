using Microsoft.AspNetCore.Mvc;

namespace TomasosPizzaApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}