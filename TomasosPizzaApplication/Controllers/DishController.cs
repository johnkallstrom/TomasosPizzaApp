﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IProduktService _produktService;
        private readonly IDishService _dishService;
        private readonly ISessionService _sessionService;

        public DishController(
            IProduktService produktService,
            IDishService dishService,
            ISessionService sessionService)
        {
            _produktService = produktService;
            _sessionService = sessionService;
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
        public IActionResult CreateDish(CreateDishViewModel data)
        {
            if (ModelState.IsValid)
            {
                data.DishIngredients = _dishService.FetchIngredientsFromSession();
                _dishService.CreateNewDish(data.Dish, data.DishIngredients);
                _sessionService.Clear();
                return RedirectToAction("ViewDishes", "Admin");
            }

            var model = new CreateDishViewModel
            {
                DishIngredients = _dishService.FetchIngredientsFromSession(),
                Ingredients = _dishService.FetchDishIngredients(),
                Categories = _dishService.FetchDishCategories()
            };

            return View(model);
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

        [HttpGet]
        public IActionResult CreateIngredient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateIngredient(Produkt ingredient)
        {
            var result = _produktService.AddIngredient(ingredient);

            if (result == true)
            {
                return RedirectToAction("ViewIngredients", "Admin");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditIngredient(int id)
        {
            var model = _produktService.FetchIngredient(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditIngredient(Produkt ingredient)
        {
            var result = _produktService.UpdateIngredient(ingredient);

            if (result == true)
            {
                return RedirectToAction("ViewIngredients", "Admin");
            }

            return View();
        }
    }
}