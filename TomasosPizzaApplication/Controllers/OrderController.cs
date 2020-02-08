using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.ViewModels;
using System.Linq;

namespace TomasosPizzaApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDishRepository _dishRepository;

        public OrderController(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public IActionResult ViewMenu()
        {
            var model = new MenuViewModel();

            model.PizzaDishes = _dishRepository.FetchPizzaDishes();
            model.PastaDishes = _dishRepository.FetchPastaDishes();
            model.SaladDishes = _dishRepository.FetchSaladDishes();

            return View(model);
        }

        public IActionResult AddItem(int id)
        {
            List<Matratt> shoppingCart;

            var selectedItem = _dishRepository.FetchDishByID(id);

            if (HttpContext.Session.GetString("Cart") == null)
            {
                shoppingCart = new List<Matratt>();
            }
            else
            {
                var JSONshoppingCart = HttpContext.Session.GetString("Cart");
                shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(JSONshoppingCart);
            }

            shoppingCart.Add(selectedItem);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(shoppingCart));

            return ViewComponent("ShoppingCart", shoppingCart);
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            var JSONshoppingCart = HttpContext.Session.GetString("Cart");
            var shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(JSONshoppingCart);

            var selectedItem = shoppingCart.FirstOrDefault(i => i.MatrattId == id);
            shoppingCart.Remove(selectedItem);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(shoppingCart));

            return ViewComponent("ShoppingCart", shoppingCart);
        }
    }
}