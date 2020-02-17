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

        public void CreateOrder(Kund customer, List<CartItemViewModel> items, int total)
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
        }

        public List<Bestallning> FetchAllOrders()
        {
            var orders = _orderRepository.FetchAll();
            orders.OrderBy(x => x.BestallningDatum);
            return orders;
        }

        public void UpdateOrderStatus(int id, bool isDelivered)
        {
            var order = _orderRepository.Fetch(id);
            order.Levererad = isDelivered;
            _orderRepository.Update(order);
        }
    }
}
