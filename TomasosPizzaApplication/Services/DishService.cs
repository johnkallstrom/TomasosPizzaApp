using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Services
{
    public class DishService : IDishService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDishRepository _dishRepository;

        public DishService(
            IHttpContextAccessor httpContextAccessor,
            IDishRepository dishRepository)
        {
            _httpContextAccessor = httpContextAccessor;
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
            return await _dishRepository.DeleteIngredient(dishID, ingredientID);
        }

        public async Task<bool> AddIngredientToDish(int dishID, int ingredientID)
        {
            return await _dishRepository.AddIngredient(dishID, ingredientID);
        }

        public void UpdateDish(Matratt dish)
        {
            _dishRepository.Update(dish);
        }

        public void AddIngredientToSession(int ingredientID)
        {
            List<Produkt> ingredients;

            var selectedIngredient = _dishRepository.FetchIngredientByID(ingredientID);

            if (_httpContextAccessor.HttpContext.Session.GetString("Ingredients") == null)
            {
                ingredients = new List<Produkt>();
            }
            else
            {
                var dataJSON = _httpContextAccessor.HttpContext.Session.GetString("Ingredients");
                ingredients = JsonConvert.DeserializeObject<List<Produkt>>(dataJSON);
            }

            ingredients.Add(selectedIngredient);

            _httpContextAccessor.HttpContext.Session.SetString("Ingredients", JsonConvert.SerializeObject(ingredients));
        }

        public void DeleteIngredientFromSession(int ingredientID)
        {
            List<Produkt> ingredients;

            if (_httpContextAccessor.HttpContext.Session.GetString("Ingredients") == null)
            {
                ingredients = new List<Produkt>();
            }
            else
            {
                var dataJSON = _httpContextAccessor.HttpContext.Session.GetString("Ingredients");
                ingredients = JsonConvert.DeserializeObject<List<Produkt>>(dataJSON);
            }

            var selectedIngredient = ingredients.FirstOrDefault(x => x.ProduktId == ingredientID);

            ingredients.Remove(selectedIngredient);

            _httpContextAccessor.HttpContext.Session.SetString("Ingredients", JsonConvert.SerializeObject(ingredients));
        }

        public List<Produkt> FetchIngredientsFromSession()
        {
            List<Produkt> ingredients;

            if (_httpContextAccessor.HttpContext.Session.GetString("Ingredients") == null)
            {
                ingredients = new List<Produkt>();
            }
            else
            {
                var dataJSON = _httpContextAccessor.HttpContext.Session.GetString("Ingredients");
                ingredients = JsonConvert.DeserializeObject<List<Produkt>>(dataJSON);
            }

            return ingredients;
        }
    }
}
