using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Services
{
    public class ProduktService : IProduktService
    {
        private readonly IProduktRepository _produktRepository;

        public ProduktService(IProduktRepository produktRepository)
        {
            _produktRepository = produktRepository;
        }

        public bool AddIngredient(Produkt produkt)
        {
            var result = false;

            _produktRepository.Add(produkt);

            result = true;
            return result;
        }

        public Produkt FetchIngredient(int id)
        {
            return _produktRepository.Fetch(id);
        }

        public bool UpdateIngredient(Produkt produkt)
        {
            var result = false;

            _produktRepository.Update(produkt);

            result = true;
            return result;
        }
    }
}
