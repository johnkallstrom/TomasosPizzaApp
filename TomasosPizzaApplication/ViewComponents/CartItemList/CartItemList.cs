using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.ViewComponents.CartItemList
{
    public class CartItemList : ViewComponent
    {
        public IViewComponentResult Invoke(List<CartItemViewModel> model)
        {
            return View("CartItemList", model);
        }
    }
}