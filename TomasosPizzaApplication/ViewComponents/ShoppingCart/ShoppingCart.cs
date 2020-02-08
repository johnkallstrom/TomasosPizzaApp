using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewComponents.ShoppingCart
{
    public class ShoppingCart : ViewComponent
    {
        public IViewComponentResult Invoke(List<Matratt> model)
        {
            return View("ShoppingCart", model);
        }
    }
}
