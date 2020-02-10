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
            List<CartItemViewModel> model;

            if (items != null)
            {
                model = items
                            .GroupBy(i => i.MatrattId)
                            .Select(x => new CartItemViewModel
                            {
                                ItemID = x.Key,
                                ItemName = x.First().MatrattNamn,
                                ItemCount = x.Count(),
                                ItemTotal = x.Sum(i => i.Pris)
                            }).ToList();

                return View("CartItemList", model);
            }
            else
            {
                model = new List<CartItemViewModel>();
                return View("CartItemList", model);
            }
        }
    }
}
