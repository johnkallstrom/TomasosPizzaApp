using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class MenuViewModel
    {
        public List<Matratt> PizzaDishes { get; set; }
        public List<Matratt> PastaDishes { get; set; }
        public List<Matratt> SaladDishes { get; set; }
        public List<CartItemViewModel> Items { get; set; }
    }
}
