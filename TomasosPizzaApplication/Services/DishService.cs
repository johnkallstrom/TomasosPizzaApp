using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public List<Matratt> FetchPizzaDishes()
        {
            var dishes = _dishRepository.FetchAllDishes();

            return dishes
                .Where(d => d.MatrattTyp == 1)
                .ToList();
        }

        public List<Matratt> FetchPastaDishes()
        {
            var dishes = _dishRepository.FetchAllDishes();

            return dishes
                .Where(d => d.MatrattTyp == 2)
                .ToList();
        }

        public List<Matratt> FetchSaladDishes()
        {
            var dishes = _dishRepository.FetchAllDishes();

            return dishes
                .Where(d => d.MatrattTyp == 3)
                .ToList();
        }
    }
}
