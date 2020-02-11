using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TomasosPizzaApplication.Models;

namespace TomasosPizzaApplication.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly TomasosContext _context;

        public DishRepository(TomasosContext context)
        {
            _context = context;
        }

        public Matratt FetchDishByID(int id)
        {
            return _context.Matratt.FirstOrDefault(m => m.MatrattId == id);
        }

        public List<Matratt> FetchAll()
        {
            return _context.Matratt
                .Include(m => m.MatrattTypNavigation)
                .Include(m => m.MatrattProdukt)
                .ThenInclude(p => p.Produkt)
                .ToList();
        }
    }
}
