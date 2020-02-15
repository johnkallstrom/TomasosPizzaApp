using System.Collections.Generic;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class CartListViewModel
    {
        public ApplicationUser User { get; set; }
        public Kund Customer { get; set; }
        public List<CartItemViewModel> Items { get; set; }
    }
}
