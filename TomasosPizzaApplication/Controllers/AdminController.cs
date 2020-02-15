using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Services;

namespace TomasosPizzaApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;

        public AdminController(
            IUserService userService,
            IAdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewUsers()
        {
            var model = _userService.FetchAllUsers();

            return View(model);
        }

        public async Task<IActionResult> UpdateUserRole(string id)
        {
            var user = await _userService.FetchUserByID(id);
            var currentCustomer = _userService.FetchCurrentCustomer(user.Id);

            var isRegular = await _userService.IsUserRegular(user);
            if (isRegular == true)
            {
                var result = await _adminService.AddToPremiumRole(user);

                if (result.Succeeded)
                {
                    currentCustomer.BonusPoints = 0;
                    _userService.UpdateUserDetails(user, currentCustomer);

                    var model = _userService.FetchAllUsers();
                    return ViewComponent("UserRoleTable", model);
                }
            }

            var isPremium = await _userService.IsUserPremium(user);
            if (isPremium == true)
            {
                var result = await _adminService.AddToRegularRole(user);

                if (result.Succeeded)
                {
                    currentCustomer.BonusPoints = null;
                    _userService.UpdateUserDetails(user, currentCustomer);

                    var model = _userService.FetchAllUsers();
                    return ViewComponent("UserRoleTable", model);
                }
            }

            return View();
        }
    }
}