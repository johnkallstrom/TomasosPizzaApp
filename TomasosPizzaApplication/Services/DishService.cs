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
            var dishes = _dishRepository.FetchAll();

            return dishes
                .Where(d => d.MatrattTyp == 1)
                .ToList();
        }

        public List<Matratt> FetchPastaDishes()
        {
            var dishes = _dishRepository.FetchAll();

            return dishes
                .Where(d => d.MatrattTyp == 2)
                .ToList();
        }

        public List<Matratt> FetchSaladDishes()
        {
            var dishes = _dishRepository.FetchAll();

            return dishes
                .Where(d => d.MatrattTyp == 3)
                .ToList();
        }

        public List<Matratt> FetchAllDishes()
        {
            return _dishRepository.FetchAll();
        }

        public Matratt FetchDish(int id)
        {
            return _dishRepository.FetchDishByID(id);
        }

        public List<MatrattTyp> FetchDishCategories()
        {
            return _dishRepository.FetchDishCategories();
        }

        public List<Produkt> FetchDishIngredients()
        {
            return _dishRepository.FetchDishIngredients();
        }

        public void DeleteIngredientFromDish(int dishID, int ingredientID)
        {
            _dishRepository.DeleteIngredientFromDish(dishID, ingredientID);
        }

        public void AddIngredientToDish(int dishID, int ingredientID)
        {
            _dishRepository.AddIngredientToDish(dishID, ingredientID);
        }

        public void UpdateDish(Matratt dish)
        {
            _dishRepository.Update(dish);
        }
    }
}
