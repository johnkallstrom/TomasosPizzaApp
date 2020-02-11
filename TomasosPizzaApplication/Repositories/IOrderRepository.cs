using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IOrderRepository
    {
        void Add(Bestallning order);
    }
}
