using Microsoft.AspNetCore.Mvc;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.ViewComponents.CreateDishIngredients
{
    public class CreateDishIngredients : ViewComponent
    {
        public IViewComponentResult Invoke(CreateDishViewModel model)
        {
            return View("CreateDishIngredients", model);
        }
    }
}
