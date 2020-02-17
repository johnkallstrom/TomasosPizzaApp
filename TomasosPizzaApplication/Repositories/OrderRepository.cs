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

        public void Delete(int id)
        {
            var order = _context.Bestallning.FirstOrDefault(x => x.BestallningId == id);

            foreach (var dish in order.BestallningMatratt)
            {
                _context.BestallningMatratt.Remove(dish);
            }

            _context.Bestallning.Remove(order);
            _context.SaveChanges();
        }

        public Bestallning Fetch(int id)
        {
            return _context.Bestallning
                .Include(o => o.BestallningMatratt)
                .FirstOrDefault(x => x.BestallningId == id);
        }

        public List<Bestallning> FetchAll()
        {
            return _context.Bestallning
                .Include("Kund")
                .Include(o => o.BestallningMatratt)
                .ThenInclude(x => x.Matratt)
                .OrderByDescending(o => o.BestallningDatum)
                .ToList();
        }

        public void Update(Bestallning order)
        {
            var currentOrder = _context.Bestallning.FirstOrDefault(o => o.BestallningId == order.BestallningId);

            _context.Entry(currentOrder).CurrentValues.SetValues(order);
            _context.SaveChanges();
        }
    }
}
