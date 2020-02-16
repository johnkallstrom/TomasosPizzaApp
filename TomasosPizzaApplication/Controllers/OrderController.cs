using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDishService _dishService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public OrderController(
            IUserService userService,
            ICartService cartService,
            IDishService dishService,
            IOrderService orderService)
        {
            _userService = userService;
            _cartService = cartService;
            _dishService = dishService;
            _orderService = orderService;
        }

        [Authorize]
        public IActionResult ViewMenu()
        {
            var model = new MenuViewModel()
            {
                PizzaDishes = _dishService.FetchPizzaDishes(),
                PastaDishes = _dishService.FetchPastaDishes(),
                SaladDishes = _dishService.FetchSaladDishes(),
                Items = _cartService.FetchGroupedCartItems()
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userService.FetchCurrentUser();

            var model = new CheckoutViewModel()
            {
                Items = _cartService.FetchGroupedCartItems(),
                User = user,
                Kund = _userService.FetchCurrentCustomer(user.Id),
                BonusPoints = _cartService.CalculateBonusPoints(),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            model.Items = _cartService.FetchGroupedCartItems();
            model.Total = _cartService.FetchCartTotal();
            model.User = await _userService.FetchCurrentUser();
            model.BonusPoints = _cartService.CalculateBonusPoints();

            if (ModelState.IsValid)
            {
                if (await _userService.IsUserPremium(model.User) == true)
                {
                    await _userService.AddBonusPointsToUser(model.Kund, model.BonusPoints);
                }

                _orderService.CreateOrder(model.Kund, model.Items, model.Total);
                return RedirectToAction("Confirmation", "Order");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}