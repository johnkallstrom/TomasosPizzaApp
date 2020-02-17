using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.ViewComponents.OrderTable
{
    public class OrderTable : ViewComponent
    {
        public IViewComponentResult Invoke(List<Bestallning> model)
        {
            return View("OrderTable", model);
        }
    }
}
