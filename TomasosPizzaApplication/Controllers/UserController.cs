using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class UserController : Controller
    {
        private ICustomerRepository repository;

        public UserController(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = repository.IsValidUser(user.Username, user.Password);

                if (isValidUser == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Felaktigt lösenord");
                    return View();
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
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

                repository.RegisterNewCustomer(kund);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}