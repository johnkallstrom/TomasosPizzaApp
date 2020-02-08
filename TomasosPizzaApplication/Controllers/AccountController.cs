using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            IUserRepository repository,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public IActionResult Settings()
        {
            return View();
        }

        public async Task<IActionResult> UpdateDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var currentCustomer = _repository.FetchUserByID(user.Id);

            if (currentCustomer != null)
            {
                var model = new UpdateAccountViewModel();
                model.Kund = currentCustomer;
                return View(model);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDetails(UpdateAccountViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Kund.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _repository.UpdateUser(model.Kund);
                ViewBag.UpdateMessage = "Ditt konto har uppdaterats.";
                return View(model);
            }

            return View();
        }

        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            var currentCustomer = _repository.FetchUserByID(user.Id);

            var model = new ChangePasswordViewModel();
            model.Kund = currentCustomer;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Kund.UserId = user.Id;

            var isPassValid = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (isPassValid == false)
            {
                ModelState.AddModelError("CurrentPassword", "Nuvarande lösenord är inkorrekt");
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // If password change succeeded force user to login again
                return RedirectToAction("Logout", "User");
            }

            return View(model);
        }

        public async Task<IActionResult> ChangeUsername()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new ChangeUsernameViewModel();
            model.Username = user.UserName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            user.UserName = model.Username;

            var isPassValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (isPassValid == false)
            {
                ModelState.AddModelError("Password", "Lösenord är inkorrekt");
                return View(model);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // If username change succeeded force user to login again
                return RedirectToAction("Logout", "User");
            }

            return View(model);
        }
    }
}