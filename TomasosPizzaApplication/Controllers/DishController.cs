using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        public async Task<IActionResult> AddIngredient(int dishID, int ingredientID)
        {
            var result = await _dishService.AddIngredientToDish(dishID, ingredientID);

            if (result == false)
            {
                ViewBag.IngredientError = "Ingrediensen finns redan i maträtten";
            }

            var model = new EditDishViewModel
            {
                Dish = _dishService.FetchDish(dishID),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return ViewComponent("EditDishIngredients", model);
        }

        public async Task<IActionResult> DeleteIngredient(int dishID, int ingredientID)
        {
            await _dishService.DeleteIngredientFromDish(dishID, ingredientID);

            var model = new EditDishViewModel
            {
                Dish = _dishService.FetchDish(dishID),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return ViewComponent("EditDishIngredients", model);
        }

        [HttpGet]
        public IActionResult EditDish(int id)
        {
            var model = new EditDishViewModel
            {
                Dish = _dishService.FetchDish(id),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDish(EditDishViewModel data)
        {
            if (ModelState.IsValid)
            {
                _dishService.UpdateDish(data.Dish);
                return RedirectToAction("ViewDishes", "Admin");
            }

            var model = new EditDishViewModel
            {
                Dish = _dishService.FetchDish(data.Dish.MatrattId),
                Categories = _dishService.FetchDishCategories(),
                Ingredients = _dishService.FetchDishIngredients(),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateDish()
        {
            var model = new CreateDishViewModel
            {
                DishIngredients = _dishService.FetchIngredientsFromSession(),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDish(CreateDishViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        public IActionResult AddIngredientToSession(int ingredientID)
        {
            if (_dishService.IngredientExists(ingredientID) == true)
            {
                ViewBag.IngredientError = "Ingrediensen finns redan i maträtten";
            }
            else
            {
                _dishService.AddIngredientToSession(ingredientID);
            }

            var model = new CreateDishViewModel
            {
                DishIngredients = _dishService.FetchIngredientsFromSession(),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return ViewComponent("CreateDishIngredients", model);
        }


        public IActionResult DeleteIngredientFromSession(int ingredientID)
        {
            _dishService.DeleteIngredientFromSession(ingredientID);

            var model = new CreateDishViewModel
            {
                DishIngredients = _dishService.FetchIngredientsFromSession(),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return ViewComponent("CreateDishIngredients", model);
        }
    }
}