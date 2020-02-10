using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IOrderRepository
    {
        List<Bestallning> FetchAllOrders();
        List<Bestallning> FetchOrdersByCustomerID(int id);
        void AddOrder(Bestallning order);
    }
}
