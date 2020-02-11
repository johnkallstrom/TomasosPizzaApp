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

namespace TomasosPizzaApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(
            IDishRepository dishRepository,
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager)
        {
            _dishRepository = dishRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> AddItem(int id)
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
            // TODO: Display cart, total etc

            return View();
        }
    }
}