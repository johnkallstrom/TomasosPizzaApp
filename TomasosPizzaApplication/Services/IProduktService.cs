using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Services
{
    public interface IProduktService
    {
        bool AddIngredient(Produkt produkt);
        Produkt FetchIngredient(int id);
        bool UpdateIngredient(Produkt produkt);
    }
}
