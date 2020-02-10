using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.ViewComponents.CartItemList
{
    public class CartItemList : ViewComponent
    {
        public IViewComponentResult Invoke(List<Matratt> items)
        {
            var cart = new Cart();
            cart.Items = items;

            return View("CartItemList", cart.GroupCartItems());
        }
    }
}
