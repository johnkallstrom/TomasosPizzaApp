using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> DeleteIngredientFromDish(int dishID, int ingredientID)
        {
            var result = false;

            await _dishRepository.DeleteIngredientFromDish(dishID, ingredientID);
            result = true;
            return result;
        }

        public async Task<bool> AddIngredientToDish(int dishID, int ingredientID)
        {
            var result = false; 

            await _dishRepository.AddIngredientToDish(dishID, ingredientID);
            result = true;
            return result;
        }

        public async Task<bool> UpdateDish(Matratt dish)
        {
            var result = false;

            await _dishRepository.UpdateAsync(dish);
            result = true;
            return result;
        }
    }
}
