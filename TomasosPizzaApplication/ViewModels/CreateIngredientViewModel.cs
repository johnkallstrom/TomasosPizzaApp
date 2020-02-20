using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class CreateIngredientViewModel
    {
        public Produkt NewIngredient { get; set; }
        public List<Produkt> Ingredients { get; set; }
    }
}
