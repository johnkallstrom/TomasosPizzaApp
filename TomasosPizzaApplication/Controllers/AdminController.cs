using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;

        public AdminController(
            IDishService dishService,
            IOrderService orderService,
            IUserService userService,
            IAdminService adminService)
        {
            _dishService = dishService;
            _orderService = orderService;
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

        public IActionResult ViewOrders()
        {
            var model = _orderService.FetchAllOrders();

            return View(model);
        }

        public IActionResult UpdateOrderStatus(int id)
        {
            var order = _orderService.FetchOrder(id);

            var isDelivered = _orderService.isOrderDelivered(order.BestallningId);
            if (isDelivered == false)
            {
                _orderService.SetOrderAsDelivered(order);

                var model = _orderService.FetchAllOrders();

                return ViewComponent("OrderTable", model);
            }
            if (isDelivered == true)
            {
                _orderService.SetOrderAsNotDelivered(order);

                var model = _orderService.FetchAllOrders();

                return ViewComponent("OrderTable", model);
            }

            return View();
        }

        public IActionResult DeleteOrder(int id)
        {
            var order = _orderService.FetchOrder(id);

            if (order != null)
            {
                _orderService.DeleteOrder(order.BestallningId);

                var model = _orderService.FetchAllOrders();

                return ViewComponent("OrderTable", model);
            }

            return View();
        }

        public IActionResult ViewDishes()
        {
            var model = _dishService.FetchAllDishes();

            return View(model);
        }

        [HttpGet]
        public IActionResult EditDish(int id)
        {
            var model = new EditDishViewModel
            {
                Dish = _dishService.FetchDish(id),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return View(model);
        }

        public IActionResult AddIngredient(int dishID, int ingredientID)
        {
            _dishService.AddIngredientToDish(dishID, ingredientID);

            var model = new EditDishViewModel
            {
                Dish = _dishService.FetchDish(dishID),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return ViewComponent("IngredientList", model);
        }

        public IActionResult DeleteIngredient(int dishID, int ingredientID)
        {
            _dishService.DeleteIngredientFromDish(dishID, ingredientID);

            var model = new EditDishViewModel
            {
                Dish = _dishService.FetchDish(dishID),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return ViewComponent("IngredientList", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDish(EditDishViewModel data)
        {
            if (ModelState.IsValid)
            {
                _dishService.UpdateDish(data.Dish);
                return RedirectToAction("ViewDishes", "Admin");
            }

            var model = new EditDishViewModel
            {

            };

            return View();
        }
    }
}