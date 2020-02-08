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
            List<Matratt> shoppingCart;

            if (HttpContext.Session.GetString("Cart") == null)
            {
                shoppingCart = new List<Matratt>();
            }
            else
            {
                var dataJSON = HttpContext.Session.GetString("Cart");
                shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            var model = new MenuViewModel();

            model.PizzaDishes = _dishRepository.FetchPizzaDishes();
            model.PastaDishes = _dishRepository.FetchPastaDishes();
            model.SaladDishes = _dishRepository.FetchSaladDishes();
            model.ShoppingCart = shoppingCart;

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
                var dataJSON = HttpContext.Session.GetString("Cart");
                shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }


            shoppingCart.Add(selectedItem);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(shoppingCart));

            return ViewComponent("ShoppingCart", shoppingCart);
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            var dataJSON = HttpContext.Session.GetString("Cart");
            var shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);

            var selectedItem = shoppingCart.FirstOrDefault(i => i.MatrattId == id);
            shoppingCart.Remove(selectedItem);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(shoppingCart));

            return ViewComponent("ShoppingCart", shoppingCart);
        }
    }
}