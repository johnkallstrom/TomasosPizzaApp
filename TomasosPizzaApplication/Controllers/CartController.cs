using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.Services;

namespace TomasosPizzaApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly ICartService _cartService;

        public CartController(
            IDishRepository dishRepository,
            ICartService cartService)
        {
            _dishRepository = dishRepository;
            _cartService = cartService;
        }

        [Authorize]
        public IActionResult AddItem(int id)
        {
            var selectedItem = _dishRepository.FetchDishByID(id);

            _cartService.AddItemToCart(selectedItem);

            var model = _cartService.FetchCartItems();

            return ViewComponent("CartItemList", model);
        }

        [Authorize]
        public IActionResult DeleteItem(int id)
        {
            _cartService.DeleteItemFromCart(id);

            var model = _cartService.FetchCartItems();

            return ViewComponent("CartItemList", model);
        }

        [Authorize]
        public IActionResult Checkout()
        {
            var model = _cartService.FetchCartItems();

            return View(model);
        }
    }
}