using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly ICartService _cartService;

        public OrderController(
            IDishRepository dishRepository,
            ICartService cartService)
        {
            _dishRepository = dishRepository;
            _cartService = cartService;
        }

        [Authorize]
        public IActionResult ViewMenu()
        {
            List<Matratt> cart;

            if (HttpContext.Session.GetString("Cart") == null)
            {
                cart = new List<Matratt>();
            }
            else
            {
                var dataJSON = HttpContext.Session.GetString("Cart");
                cart = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            var model = new MenuViewModel();

            model.PizzaDishes = _dishRepository.FetchPizzaDishes();
            model.PastaDishes = _dishRepository.FetchPastaDishes();
            model.SaladDishes = _dishRepository.FetchSaladDishes();
            model.Items = _cartService.GroupCartItems(cart);

            return View(model);
        }
    }
}