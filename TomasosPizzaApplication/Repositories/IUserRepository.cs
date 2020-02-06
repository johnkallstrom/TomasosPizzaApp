using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IUserRepository
    {
        void AddUser(Kund kund);
        Kund FetchUserByID(string id);
        void UpdateUser(Kund kund);
        List<Kund> FetchAllUsers();
    }
}
