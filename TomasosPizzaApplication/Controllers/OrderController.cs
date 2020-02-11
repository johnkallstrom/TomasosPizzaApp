using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly ICartService _cartService;

        public OrderController(
            IDishRepository dishRepository,
            ICartService cartService)
        {
            _dishRepository = dishRepository;
            _cartService = cartService;
        }

        [Authorize]
        public IActionResult ViewMenu()
        {
            var model = new MenuViewModel();

            model.PizzaDishes = _dishRepository.FetchPizzaDishes();
            model.PastaDishes = _dishRepository.FetchPastaDishes();
            model.SaladDishes = _dishRepository.FetchSaladDishes();
            model.Items = _cartService.FetchCartItems();

            return View(model);
        }
    }
}