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
        private readonly IUserService _userService;

        public CartController(
            ICartService cartService,
            IUserService userService)
        {
            _userService = userService;
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> AddItem(int id)
        {
            var user = await _userService.FetchCurrentUser();

            _cartService.AddItemToCart(id);

            var model = new CartListViewModel
            {
                Items = _cartService.FetchGroupedCartItems(),
                User = user,
                Customer = _userService.FetchCurrentCustomer(user.Id)
            };

            return ViewComponent("CartItemList", model);
        }

        [Authorize]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var user = await _userService.FetchCurrentUser();

            _cartService.DeleteItemFromCart(id);

            var model = new CartListViewModel
            {
                Items = _cartService.FetchGroupedCartItems(),
                User = user,
                Customer = _userService.FetchCurrentCustomer(user.Id)
            };

            return ViewComponent("CartItemList", model);
        }
    }
}