using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.Repositories;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDishRepository _dishRepository;

        public CartService(
            IHttpContextAccessor httpContextAccessor,
            IDishRepository dishRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _dishRepository = dishRepository;
        }

        public void AddItemToCart(int id)
        {
            List<Matratt> items;

            var selectedItem = _dishRepository.FetchDishByID(id);

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

        public List<Matratt> FetchCartItems()
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

            return items;
        }

        public List<CartItemViewModel> FetchGroupedCartItems()
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

            var groupedItems = GroupCartItems(items);

            return groupedItems;
        }

        public int FetchCartTotal()
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

            return items.Sum(i => i.Pris);
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

        public int CalculateBonusPoints()
        {
            var items = FetchCartItems();
            return items.Count() * 10;
        }

        public bool HasPointsForFreeDish(Kund kund)
        {
            var result = false;

            var items = FetchCartItems();

            if (kund.BonusPoints >= 100 && items.Count() >= 1)
            {
                result = true;
                return result;
            }

            return result;
        }

        public int FetchDiscountCartTotal(Kund kund)
        {
            var items = FetchCartItems();
            var total = FetchCartTotal();

            if (HasPointsForFreeDish(kund) == true)
            {
                total -= items.First().Pris;
            }

            if (items.Count >= 3)
            {
                total -= (int)Math.Round(total * 0.20);
            }

            return total;
        }

        public int GetPremiumDiscount()
        {
            var total = FetchCartTotal();
            total -= GetBonusDiscount();
            return (int)Math.Round(total * 0.20);
        }

        public int GetBonusDiscount()
        {
            var items = FetchCartItems();
            return items.First().Pris;
        }
    }
}
