using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Controllers
{
    public class HomeController : Controller
    {
        private IKundRepository repository;

        public HomeController(IKundRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kund kund)
        {
            if (ModelState.IsValid)
            {
                repository.RegisterCustomer(kund);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}