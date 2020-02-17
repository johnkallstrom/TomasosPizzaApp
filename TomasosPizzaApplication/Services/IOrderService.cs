using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface IOrderService
    {
        List<Bestallning> FetchAllOrders();
        void UpdateOrderStatus(int id, bool isDelivered);
        void CreateOrder(Kund customer, List<CartItemViewModel> items, int total);
    }
}
