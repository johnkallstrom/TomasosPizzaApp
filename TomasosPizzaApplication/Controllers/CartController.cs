using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

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

            var model = _cartService.FetchGroupedCartItems();

            return ViewComponent("CartItemList", model);
        }

        [Authorize]
        public IActionResult DeleteItem(int id)
        {
            _cartService.DeleteItemFromCart(id);

            var model = _cartService.FetchGroupedCartItems();

            return ViewComponent("CartItemList", model);
        }
    }
}