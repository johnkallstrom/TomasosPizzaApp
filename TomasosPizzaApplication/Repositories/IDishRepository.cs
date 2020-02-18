using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IDishRepository
    {
        void Update(Matratt updatedDish);
        void DeleteIngredientFromDish(int dishID, int ingredientID);
        void AddIngredientToDish(int dishID, int ingredientID);
        Produkt FetchIngredientByID(int id);
        List<MatrattTyp> FetchDishCategories();
        List<Produkt> FetchDishIngredients();
        List<Matratt> FetchAll();
        Matratt FetchDishByID(int id);
    }
}
