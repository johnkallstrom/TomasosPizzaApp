using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.ViewComponents.EditDishIngredients
{
    public class EditDishIngredients : ViewComponent
    {
        public IViewComponentResult Invoke(EditDishViewModel model)
        {
            return View("EditDishIngredients", model);
        }
    }
}
