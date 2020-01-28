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

        [HttpPost]
        public IActionResult Login(Kund kund)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Kund kund)
        {
            if (ModelState.IsValid)
            {
                if (repository.UsernameAlreadyExists(kund) == true)
                {
                    ModelState.AddModelError("AnvandarNamn", "Användarnamnet är upptaget");
                    return View();
                }

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