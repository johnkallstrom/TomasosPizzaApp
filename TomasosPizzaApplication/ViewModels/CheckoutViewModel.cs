using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> Items { get; set; }
        public Kund Kund { get; set; }
        public int Total { get; set; }
    }
}
