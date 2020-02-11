using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.Services;

namespace TomasosPizzaApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public IActionResult AddItem(int id)
        {
            _cartService.AddItemToCart(id);

            var items = _cartService.FetchCartItems();
            var model = _cartService.GroupCartItems(items);

            return ViewComponent("CartItemList", model);
        }

        [Authorize]
        public IActionResult DeleteItem(int id)
        {
            _cartService.DeleteItemFromCart(id);

            var items = _cartService.FetchCartItems();
            var model = _cartService.GroupCartItems(items);

            return ViewComponent("CartItemList", model);
        }

        [Authorize]
        public IActionResult Checkout()
        {
            var items = _cartService.FetchCartItems();
            var model = _cartService.GroupCartItems(items);

            return View(model);
        }
    }
}