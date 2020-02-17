using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class EditDishViewModel
    {
        public Matratt Dish { get; set; }
        public List<Produkt> Ingredients { get; set; }
        public List<MatrattTyp> Categories { get; set; }
    }
}
