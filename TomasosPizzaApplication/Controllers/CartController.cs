using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.Services;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly ICartService _cartService;

        public CartController(
            IDishRepository dishRepository,
            ICartService cartService)
        {
            _dishRepository = dishRepository;
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> AddItem(int id)
        {
            List<Matratt> cart;

            var selectedItem = _dishRepository.FetchDishByID(id);

            if (HttpContext.Session.GetString("Cart") == null)
            {
                cart = new List<Matratt>();
            }
            else
            {
                var dataJSON = HttpContext.Session.GetString("Cart");
                cart = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            cart.Add(selectedItem);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            var model = _cartService.GroupCartItems(cart);

            return ViewComponent("CartItemList", model);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Checkout()
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

            var model = _cartService.GroupCartItems(cart);

            return View(model);
        }
    }
}