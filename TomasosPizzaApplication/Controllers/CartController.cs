using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;

namespace TomasosPizzaApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly IDishRepository _dishRepository;

        public CartController(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
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

        public IActionResult DeleteItem(int id)
        {
            List<Matratt> shoppingCart;

            var dataJSON = HttpContext.Session.GetString("Cart");
            shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);

            var selectedItem = shoppingCart.FirstOrDefault(i => i.MatrattId == id);
            shoppingCart.Remove(selectedItem);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(shoppingCart));

            return ViewComponent("ShoppingCart", shoppingCart);
        }
    }
}