using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Services
{
    public interface IDishService
    {
        List<Matratt> FetchAllDishes();
        List<Matratt> FetchPizzaDishes();
        List<Matratt> FetchPastaDishes();
        List<Matratt> FetchSaladDishes();
    }
}
