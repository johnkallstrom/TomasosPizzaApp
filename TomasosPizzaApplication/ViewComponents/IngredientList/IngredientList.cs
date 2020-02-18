using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.ViewComponents.IngredientList
{
    public class IngredientList : ViewComponent
    {
        public IViewComponentResult Invoke(EditDishViewModel model)
        {
            return View("IngredientList", model);
        }
    }
}
