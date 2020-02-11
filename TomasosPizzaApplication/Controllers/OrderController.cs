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

        public OrderController(
            ICartService cartService,
            IDishService dishService)
        {
            _cartService = cartService;
            _dishService = dishService;
        }

        [Authorize]
        public IActionResult ViewMenu()
        {
            var model = new MenuViewModel();

            model.PizzaDishes = _dishService.FetchPizzaDishes();
            model.PastaDishes = _dishService.FetchPastaDishes();
            model.SaladDishes = _dishService.FetchSaladDishes();
            model.Items = _cartService.FetchCartItems();

            return View(model);
        }

        public IActionResult AddOrder(Bestallning order)
        {
            return View();
        }
    }
}