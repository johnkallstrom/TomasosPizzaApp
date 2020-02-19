using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return _context.Matratt
                .Include(m => m.MatrattTypNavigation)
                .Include(m => m.MatrattProdukt)
                .ThenInclude(p => p.Produkt)
                .FirstOrDefault(m => m.MatrattId == id);
        }

        public List<Matratt> FetchAll()
        {
            return _context.Matratt
                .Include(m => m.MatrattTypNavigation)
                .Include(m => m.MatrattProdukt)
                .ThenInclude(p => p.Produkt)
                .ToList();
        }

        public List<MatrattTyp> FetchDishCategories()
        {
            return _context.MatrattTyp.ToList();
        }

        public List<Produkt> FetchDishIngredients()
        {
            return _context.Produkt.ToList();
        }

        public Produkt FetchIngredientByID(int id)
        {
            return _context.Produkt.FirstOrDefault(x => x.ProduktId == id);
        }

        public async Task<bool> AddIngredient(int dishID, int ingredientID)
        {
            var result = false;

            var dishIngredient = new MatrattProdukt
            {
                MatrattId = dishID,
                ProduktId = ingredientID
            };

            var ingredientExists = _context.MatrattProdukt
                .Any(x => x.MatrattId == dishID && x.ProduktId == ingredientID);

            if (ingredientExists == false)
            {
                _context.MatrattProdukt.Add(dishIngredient);
                await _context.SaveChangesAsync();
                result = true;
                return result;
            }

            return result;
        }

        public async Task<bool> DeleteIngredient(int dishID, int ingredientID)
        {
            var result = false;
            var dishIngredient = _context.MatrattProdukt
                .FirstOrDefault(x => x.MatrattId == dishID && x.ProduktId == ingredientID);

            if (dishIngredient != null)
            {
                _context.MatrattProdukt.Remove(dishIngredient);
                await _context.SaveChangesAsync();
                result = true;
                return result;
            }

            return result;
        }

        public void Update(Matratt updatedDish)
        {
            var currentDish = _context.Matratt.FirstOrDefault(x => x.MatrattId == updatedDish.MatrattId);

            if (updatedDish != null)
            {
                _context.Entry(currentDish).CurrentValues.SetValues(updatedDish);
                _context.SaveChanges();
            }
        }
    }
}
