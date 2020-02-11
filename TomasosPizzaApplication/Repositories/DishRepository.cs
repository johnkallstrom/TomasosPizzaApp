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

        public List<Matratt> FetchPizzaDishes()
        {
            return _context.Matratt
                .Where(m => m.MatrattTyp == 1)
                .Include(m => m.MatrattTypNavigation)
                .Include(m => m.MatrattProdukt)
                .ThenInclude(p => p.Produkt)
                .ToList();
        }

        public List<Matratt> FetchPastaDishes()
        {
            return _context.Matratt
                .Where(m => m.MatrattTyp == 2)
                .Include(m => m.MatrattTypNavigation)
                .Include(m => m.MatrattProdukt)
                .ThenInclude(p => p.Produkt)
                .ToList();
        }

        public List<Matratt> FetchSaladDishes()
        {
            return _context.Matratt
                 .Where(m => m.MatrattTyp == 3)
                 .Include(m => m.MatrattTypNavigation)
                 .Include(m => m.MatrattProdukt)
                 .ThenInclude(p => p.Produkt)
                 .ToList();
        }

        public Matratt FetchDishByID(int id)
        {
            return _context.Matratt.FirstOrDefault(m => m.MatrattId == id);
        }

        public List<Matratt> FetchAllDishes()
        {
            return _context.Matratt
                .Include(m => m.MatrattTypNavigation)
                .Include(m => m.MatrattProdukt)
                .ThenInclude(p => p.Produkt)
                .ToList();
        }
    }
}
