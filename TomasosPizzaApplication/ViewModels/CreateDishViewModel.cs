using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class CreateDishViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Category { get; set; }
        public List<Produkt> DishIngredients { get; set; }
        public List<Produkt> Ingredients { get; set; }
        public List<MatrattTyp> Categories { get; set; }
    }
}
