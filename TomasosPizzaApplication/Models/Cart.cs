using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.IdentityData;
using TomasosPizzaApplication.ViewModels;

namespace TomasosPizzaApplication.Models
{
    public class Cart
    {
        public List<Matratt> Items { get; set; }
        public ApplicationUser User { get; set; }
        public Kund Customer { get; set; }
        public int Total { get; set; }

        public List<CartItemViewModel> GroupCartItems()
        {
            var result = Items
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
