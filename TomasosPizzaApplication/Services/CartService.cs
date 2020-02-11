using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Services
{
    public class CartService : ICartService
    {
        public List<CartItemViewModel> GroupCartItems(List<Matratt> items)
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
