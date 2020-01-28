using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IKundRepository
    {
        void RegisterCustomer(Kund kund);
        bool UsernameAlreadyExists(Kund kund);
        Kund SignIn(Kund kund);
    }
}
