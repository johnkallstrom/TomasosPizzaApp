using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface ICartService
    {
        List<CartItemViewModel> FetchCartItems();
        void AddItemToCart(Matratt selectedItem);
        void DeleteItemFromCart(int id);
    }
}
