using System;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public OrderService(
            ICartService cartService, 
            IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }
    }
}
