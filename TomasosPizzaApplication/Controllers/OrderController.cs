using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDishService _dishService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public OrderController(
            ICartService cartService,
            IDishService dishService,
            IOrderService orderService)
        {
            _cartService = cartService;
            _dishService = dishService;
            _orderService = orderService;
        }

        [Authorize]
        public IActionResult ViewMenu()
        {
            var items = _cartService.FetchCartItems();

            var model = new MenuViewModel();

            model.PizzaDishes = _dishService.FetchPizzaDishes();
            model.PastaDishes = _dishService.FetchPastaDishes();
            model.SaladDishes = _dishService.FetchSaladDishes();
            model.Items = _cartService.GroupCartItems(items);

            return View(model);
        }

        public IActionResult AddOrder()
        {
            var order = _orderService.CreateOrder();

            return View(order);
        }
    }
}