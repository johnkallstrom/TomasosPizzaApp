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
            List<Matratt> model;

            var selectedItem = _dishRepository.FetchDishByID(id);

            if (HttpContext.Session.GetString("Cart") == null)
            {
                model = new List<Matratt>();
            }
            else
            {
                var dataJSON = HttpContext.Session.GetString("Cart");
                model = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            model.Add(selectedItem);

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(model));

            return ViewComponent("CartItemList", model);
        }

        public IActionResult DeleteItem(int id)
        {
            List<Matratt> model;

            var dataJSON = HttpContext.Session.GetString("Cart");
            model = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);

            var selectedItem = model.FirstOrDefault(i => i.MatrattId == id);
            model.Remove(selectedItem);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(model));

            return ViewComponent("CartItemList", model);
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}