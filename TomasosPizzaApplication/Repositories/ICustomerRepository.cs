using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Repositories
{
    public interface ICustomerRepository
    {
        void RegisterNewCustomer(Kund kund);
        bool UsernameAlreadyExists(Kund kund);
        bool IsValidUser(string username, string password);
    }
}
