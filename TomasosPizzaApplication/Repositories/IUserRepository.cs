using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IUserRepository
    {
        void AddCustomer(Kund kund);
        Kund GetCustomerByID(string id);
    }
}
