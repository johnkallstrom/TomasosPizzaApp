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

        public async Task<IActionResult> EditDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var currentCustomer = _repository.GetCustomerByID(user.Id);

            if (currentCustomer != null)
            {
                var model = new EditDetailsViewModel();
                model.Kund = currentCustomer;
                return View(model);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetails(EditDetailsViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Kund.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _repository.UpdateCustomer(model.Kund);
                ViewBag.UpdateMessage = "Ditt konto har uppdaterats.";
                return View(model);
            }

            return View();
        }

        public async Task<IActionResult> EditAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            var currentCustomer = _repository.GetCustomerByID(user.Id);

            var model = new EditAccountViewModel();
            model.Username = user.UserName;
            model.Kund = currentCustomer;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(EditAccountViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Kund.UserId = user.Id;

            var isPassValid = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (isPassValid == false)
            {
                ModelState.AddModelError("CurrentPassword", "Nuvarande lösenord är inkorrekt");
                return View(model);
            }

            if(!string.IsNullOrWhiteSpace(model.NewPassword) && !string.IsNullOrWhiteSpace(model.ConfirmNewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    ViewBag.UpdateMessage = "Ditt konto har uppdaterats.";
                }
            }

            user.UserName = model.Username;
            return View(model);
        }
    }
}