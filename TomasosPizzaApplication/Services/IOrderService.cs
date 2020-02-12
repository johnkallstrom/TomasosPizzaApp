using System.Collections.Generic;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface IOrderService
    {
        Bestallning CreateOrder(Kund customer, List<CartItemViewModel> items, int total);
    }
}
