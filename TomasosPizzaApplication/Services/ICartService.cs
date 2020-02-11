using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface ICartService
    {
        List<CartItemViewModel> GroupCartItems(List<Matratt> items);
    }
}
