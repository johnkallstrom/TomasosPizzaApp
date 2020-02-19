using System.Collections.Generic;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Services
{
    public interface IDishService
    {
        bool IngredientExists(int ingredientID);
        List<Produkt> FetchIngredientsFromSession();
        void AddIngredientToSession(int ingredientID);
        void DeleteIngredientFromSession(int ingredientID);
        void UpdateDish(Matratt dish);
        Task<bool> DeleteIngredientFromDish(int dishID, int ingredientID);
        Task<bool> AddIngredientToDish(int dishID, int ingredientID);
        List<MatrattTyp> FetchDishCategories();
        List<Produkt> FetchDishIngredients();
        Matratt FetchDish(int id);
        List<Matratt> FetchAllDishes();
        List<Matratt> FetchPizzaDishes();
        List<Matratt> FetchPastaDishes();
        List<Matratt> FetchSaladDishes();
    }
}
