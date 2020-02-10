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

        public void AddOrder(Bestallning order)
        {
            _context.Bestallning.Add(order);
            _context.SaveChanges();
        }

        public List<Bestallning> FetchAllOrders()
        {
            return _context.Bestallning
                .Include(b => b.BestallningMatratt)
                .ThenInclude(m => m.Matratt)
                .ToList();
        }

        public List<Bestallning> FetchOrdersByCustomerID(int id)
        {
            return _context.Bestallning
                .Where(b => b.KundId == id)
                .Include(b => b.BestallningMatratt)
                .ThenInclude(m => m.Matratt)
                .ToList();
        }
    }
}
