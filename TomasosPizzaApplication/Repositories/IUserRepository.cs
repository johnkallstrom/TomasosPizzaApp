using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IUserRepository
    {
        void Add(Kund kund);
        Kund Fetch(string id);
        void Update(Kund kund);
        List<Kund> FetchAll();
    }
}
