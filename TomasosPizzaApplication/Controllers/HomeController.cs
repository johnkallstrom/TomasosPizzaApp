using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }
    }
}