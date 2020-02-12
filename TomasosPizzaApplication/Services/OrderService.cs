using System;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly ISessionService _sessionService;
        private readonly IOrderRepository _orderRepository;

        public OrderService(
            ISessionService sessionService,
            IOrderRepository orderRepository)
        {
            _sessionService = sessionService;
            _orderRepository = orderRepository;
        }

        public Bestallning CreateOrder(Kund customer, List<CartItemViewModel> items, int total)
        {
            var orderItems = new List<BestallningMatratt>();

            items.ToList().ForEach(x => orderItems.Add(new BestallningMatratt
            {
                MatrattId = x.ItemID,
                Antal = x.ItemCount
            }));

            var order = new Bestallning()
            {
                KundId = customer.KundId,
                BestallningDatum = DateTime.Now,
                Levererad = false,
                Totalbelopp = total,
                BestallningMatratt = orderItems
            };

            _orderRepository.Add(order);
            _sessionService.Clear();
            return order;
        }
    }
}
