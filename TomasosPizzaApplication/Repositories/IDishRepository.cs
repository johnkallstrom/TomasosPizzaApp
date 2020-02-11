using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public interface IDishRepository
    {
        List<Matratt> FetchAll();
        Matratt FetchDishByID(int id);
    }
}
