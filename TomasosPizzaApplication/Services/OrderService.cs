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

        public async Task<Bestallning> CreateOrder()
        {
            var user = await _userService.FetchCurrentUser();
            var customer = _userService.FetchCurrentCustomer(user.Id);
            var cartItems = _cartService.FetchCartItems();

            var orderItems = cartItems
                        .GroupBy(i => i.MatrattId)
                        .Select(x => new BestallningMatratt
                        {
                            MatrattId = x.Key,
                            Antal = x.Count(),
                        }).ToList();

            var order = new Bestallning
            {
                KundId = customer.KundId,
                BestallningDatum = DateTime.Now,
                Levererad = false,
                Totalbelopp = cartItems.Sum(i => i.Pris),
                BestallningMatratt = orderItems,
            };

            return order;
        }
    }
}
