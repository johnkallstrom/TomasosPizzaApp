using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface IOrderService
    {
        void SetOrderAsNotDelivered(Bestallning order);
        void SetOrderAsDelivered(Bestallning order);
        Bestallning FetchOrder(int id);
        bool isOrderDelivered(int id);
        List<Bestallning> FetchAllOrders();
        void CreateOrder(Kund customer, List<CartItemViewModel> items, int total);
    }
}
