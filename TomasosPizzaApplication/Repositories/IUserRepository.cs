using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IUserRepository
    {
        void AddCustomer(Kund kund);
    }
}
