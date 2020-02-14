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
            var model = new MenuViewModel();

            model.PizzaDishes = _dishService.FetchPizzaDishes();
            model.PastaDishes = _dishService.FetchPastaDishes();
            model.SaladDishes = _dishService.FetchSaladDishes();
            model.Items = _cartService.FetchGroupedCartItems();

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
                Kund = _userService.FetchCurrentCustomer(user.Id),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            model.Items = _cartService.FetchGroupedCartItems();
            model.Total = _cartService.FetchCartTotal();

            if (ModelState.IsValid)
            {
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