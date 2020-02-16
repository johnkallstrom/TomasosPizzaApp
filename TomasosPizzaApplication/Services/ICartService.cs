using System.Collections.Generic;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public interface ICartService
    {
        int GetBonusDiscount();
        int GetPremiumDiscount();
        bool HasPointsForFreeDish(Kund kund);
        int CalculateBonusPoints();
        int FetchDiscountCartTotal(Kund kund);
        int FetchCartTotal();
        List<CartItemViewModel> FetchGroupedCartItems();
        List<Matratt> FetchCartItems();
        void AddItemToCart(int id);
        void DeleteItemFromCart(int id);
    }
}
