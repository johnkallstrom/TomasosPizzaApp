using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public class CartService : ICartService
    {
        IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddItemToCart(Matratt selectedItem)
        {
            List<Matratt> items;

            if (_httpContextAccessor.HttpContext.Session.GetString("Cart") == null)
            {
                items = new List<Matratt>();
            }
            else
            {
                var dataJSON = _httpContextAccessor.HttpContext.Session.GetString("Cart");
                items = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            items.Add(selectedItem);

            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(items));
        }

        public void DeleteItemFromCart(int id)
        {
            List<Matratt> items;

            if (_httpContextAccessor.HttpContext.Session.GetString("Cart") == null)
            {
                items = new List<Matratt>();
            }
            else
            {
                var dataJSON = _httpContextAccessor.HttpContext.Session.GetString("Cart");
                items = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            var selectedItem = items.FirstOrDefault(i => i.MatrattId == id);

            items.Remove(selectedItem);

            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(items));
        }

        public List<CartItemViewModel> FetchCartItems()
        {
            List<Matratt> items;

            if (_httpContextAccessor.HttpContext.Session.GetString("Cart") == null)
            {
                items = new List<Matratt>();
            }
            else
            {
                var dataJSON = _httpContextAccessor.HttpContext.Session.GetString("Cart");
                items = JsonConvert.DeserializeObject<List<Matratt>>(dataJSON);
            }

            var result = GroupCartItems(items);

            return result;
        }

        private List<CartItemViewModel> GroupCartItems(List<Matratt> items)
        {
            var result = items
                        .GroupBy(i => i.MatrattId)
                        .Select(x => new CartItemViewModel
                        {
                            ItemID = x.Key,
                            ItemName = x.First().MatrattNamn,
                            ItemCount = x.Count(),
                            ItemTotal = x.Sum(i => i.Pris)
                        }).ToList();

            return result;
        }
    }
}
