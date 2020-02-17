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

        public void DeleteOrder(int id)
        {
            _orderRepository.Delete(id);
        }

        public List<Bestallning> FetchAllOrders()
        {
            return _orderRepository.FetchAll();
        }

        public Bestallning FetchOrder(int id)
        {
            return _orderRepository.Fetch(id);
        }

        public bool isOrderDelivered(int id)
        {
            var result = false;

            var order = _orderRepository.Fetch(id);

            if (order.Levererad == true)
            {
                result = true;
                return result;
            }

            return result;
        }

        public void SetOrderAsDelivered(Bestallning order)
        {
            order.Levererad = true;
            _orderRepository.Update(order);
        }

        public void SetOrderAsNotDelivered(Bestallning order)
        {
            order.Levererad = false;
            _orderRepository.Update(order);
        }
    }
}
