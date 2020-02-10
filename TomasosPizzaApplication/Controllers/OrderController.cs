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
            List<Matratt> items;

            if (HttpContext.Session.GetString("Cart") == null)
            {
                items = new List<Matratt>();
            }
            else
            {
                var dataJSON = HttpContext.Session.GetString("Cart");
                items = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            var model = new MenuViewModel();

            model.PizzaDishes = _dishRepository.FetchPizzaDishes();
            model.PastaDishes = _dishRepository.FetchPastaDishes();
            model.SaladDishes = _dishRepository.FetchSaladDishes();
            model.CartItems = items
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