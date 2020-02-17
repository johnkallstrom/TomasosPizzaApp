using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IDishRepository
    {
        List<MatrattTyp> FetchDishCategories();
        List<Produkt> FetchDishIngredients();
        List<Matratt> FetchAll();
        Matratt FetchDishByID(int id);
    }
}
