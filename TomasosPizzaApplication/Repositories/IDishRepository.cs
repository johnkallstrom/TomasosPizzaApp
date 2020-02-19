using System.Collections.Generic;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IDishRepository
    {
        void Add(Matratt dish, List<Produkt> ingredients);
        void Update(Matratt dish);
        Task<bool> DeleteIngredient(int dishID, int ingredientID);
        Task<bool> AddIngredient(int dishID, int ingredientID);
        Produkt FetchIngredientByID(int id);
        List<MatrattTyp> FetchDishCategories();
        List<Produkt> FetchDishIngredients();
        List<Matratt> FetchAll();
        Matratt FetchDishByID(int id);
    }
}
