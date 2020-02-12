using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewModels
{
    public class OrderViewModel
    {
        public Kund Customer { get; set; }
        public Bestallning Order { get; set; }
    }
}
