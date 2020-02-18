using System.Collections.Generic;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IDishRepository
    {
        Task<bool> UpdateAsync(Matratt updatedDish);
        Task<bool> DeleteIngredientFromDish(int dishID, int ingredientID);
        Task<bool> AddIngredientToDish(int dishID, int ingredientID);
        Produkt FetchIngredientByID(int id);
        List<MatrattTyp> FetchDishCategories();
        List<Produkt> FetchDishIngredients();
        List<Matratt> FetchAll();
        Matratt FetchDishByID(int id);
    }
}
