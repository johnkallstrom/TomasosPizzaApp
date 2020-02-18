using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Services
{
    public interface IDishService
    {
        void UpdateDish(Matratt dish);
        void DeleteIngredientFromDish(int dishID, int ingredientID);
        void AddIngredientToDish(int dishID, int ingredientID);
        List<MatrattTyp> FetchDishCategories();
        List<Produkt> FetchDishIngredients();
        Matratt FetchDish(int id);
        List<Matratt> FetchAllDishes();
        List<Matratt> FetchPizzaDishes();
        List<Matratt> FetchPastaDishes();
        List<Matratt> FetchSaladDishes();
    }
}
