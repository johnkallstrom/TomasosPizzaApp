﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdateDetails()
        {
            var user = await _userService.FetchCurrentUser();

            var model = new UpdateDetailsViewModel();
            model.Kund = _userService.FetchCurrentCustomer(user.Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UpdateDetails(UpdateDetailsViewModel model)
        {
            var user = await _userService.FetchCurrentUser();

            if (ModelState.IsValid)
            {
                _userService.UpdateUserDetails(user, model.Kund);
                ViewBag.UpdateMessage = "Ditt konto har uppdaterats.";
                return View(model);
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdatePassword()
        {
            var user = await _userService.FetchCurrentUser();

            var model = new UpdatePasswordViewModel();
            model.Kund = _userService.FetchCurrentCustomer(user.Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model)
        {
            var user = await _userService.FetchCurrentUser();
            var result = await _userService.UpdatePassword(user, model.Kund, model.CurrentPassword, model.NewPassword);

            if (result == false)
            {
                ModelState.AddModelError("CurrentPassword", "Nuvarande lösenord är inkorrekt");
                return View(model);
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdateUsername()
        {
            var user = await _userService.FetchCurrentUser();

            var model = new UpdateUsernameViewModel();
            model.Username = user.UserName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UpdateUsername(UpdateUsernameViewModel model)
        {
            var user = await _userService.FetchCurrentUser();
            var result = await _userService.UpdateUsername(user, model.Password, model.Username);

            if (result == false)
            {
                ModelState.AddModelError("Password", "Lösenord är inkorrekt");
                return View(model);
            }
            else
            {
                return RedirectToAction("Logout", "User");
            }
        }
    }
}