using Microsoft.AspNetCore.Mvc;

namespace TomasosPizzaApplication.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}