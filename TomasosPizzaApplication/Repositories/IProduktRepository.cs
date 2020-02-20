using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IProduktRepository
    {
        void Add(Produkt produkt);
        Produkt Fetch(int id);
        void Update(Produkt produkt);
    }
}
