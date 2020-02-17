using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IOrderRepository
    {
        void Add(Bestallning order);
        void Update(Bestallning order);
        void Delete(int id);
        List<Bestallning> FetchAll();
        Bestallning Fetch(int id);
    }
}
