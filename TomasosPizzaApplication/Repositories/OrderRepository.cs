using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TomasosContext _context;

        public OrderRepository(TomasosContext context)
        {
            _context = context;
        }

        public void Add(Bestallning order)
        {
            _context.Bestallning.Add(order);
            _context.SaveChanges();
        }

        public List<Bestallning> FetchAll()
        {
            return _context.Bestallning
                .Include(o => o.BestallningMatratt)
                .ToList();
        }
    }
}
