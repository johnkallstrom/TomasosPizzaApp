using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface IOrderService
    {
        Bestallning FetchLatestOrder();
        void CreateOrder(Kund customer, List<CartItemViewModel> items, int total);
    }
}
