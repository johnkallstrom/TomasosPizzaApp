using System.Collections.Generic;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface ICartService
    {
        int FetchCartTotal();
        List<CartItemViewModel> FetchCartItems();
        void AddItemToCart(int id);
        void DeleteItemFromCart(int id);
    }
}
