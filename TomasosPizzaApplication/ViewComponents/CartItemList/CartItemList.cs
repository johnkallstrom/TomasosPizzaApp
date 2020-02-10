using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewComponents.CartItemList
{
    public class CartItemList : ViewComponent
    {
        public IViewComponentResult Invoke(List<Matratt> model)
        {
            return View("CartItemList", model);
        }
    }
}
