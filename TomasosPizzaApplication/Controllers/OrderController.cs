using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.ViewModels;

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
            var cart = new Cart();

            if (HttpContext.Session.GetString("Cart") == null)
            {
                cart.Items = new List<Matratt>();
            }
            else
            {
                var dataJSON = HttpContext.Session.GetString("Cart");
                cart.Items = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            var model = new MenuViewModel();

            model.PizzaDishes = _dishRepository.FetchPizzaDishes();
            model.PastaDishes = _dishRepository.FetchPastaDishes();
            model.SaladDishes = _dishRepository.FetchSaladDishes();
            model.Items = cart.GroupCartItems();

            return View(model);
        }
    }
}