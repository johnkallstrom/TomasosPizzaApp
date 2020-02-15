using System.Collections.Generic;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> Items { get; set; }
        public Kund Kund { get; set; }
        public ApplicationUser User { get; set; }
        public int BonusPoints { get; set; }
        public int Total { get; set; }
    }
}
