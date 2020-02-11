using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
            model.Items = cart
                            .GroupBy(i => i.MatrattId)
                            .Select(x => new CartItemViewModel
                            {
                                ItemID = x.Key,
                                ItemName = x.First().MatrattNamn,
                                ItemCount = x.Count(),
                                ItemTotal = x.Sum(i => i.Pris)
                            }).ToList();

            return View(model);
        }
    }
}