using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Settings()
        {
            return View();
        }

        public async Task<IActionResult> UpdateDetails()
        {
            var user = await _userService.FetchCurrentUser();

            var model = new UpdateAccountViewModel();
            model.Kund = _userService.FetchCurrentCustomer(user.Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDetails(UpdateAccountViewModel model)
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

        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userService.FetchCurrentUser();

            var model = new ChangePasswordViewModel();
            model.Kund = _userService.FetchCurrentCustomer(user.Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
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

        public async Task<IActionResult> ChangeUsername()
        {
            var user = await _userService.FetchCurrentUser();

            var model = new ChangeUsernameViewModel();
            model.Username = user.UserName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameViewModel model)
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