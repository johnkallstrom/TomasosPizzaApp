using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface ICartService
    {
        int CalculateBonusPoints();
        int FetchCartTotal();
        List<CartItemViewModel> FetchGroupedCartItems();
        List<Matratt> FetchCartItems();
        void AddItemToCart(int id);
        void DeleteItemFromCart(int id);
    }
}
