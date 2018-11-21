using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Database;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Domain.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly FoodTrackerContext _context;

        public IngredientsService(FoodTrackerContext context)
        {
            _context = context;
        }

        public async Task<Ingredient> CreateIngredient(Ingredient newIngredient)
        {
            if (_context.Ingredients.Any(i => i.Name == newIngredient.Name))
            {
                return await _context.Ingredients
                    .Where(i => i.Name == newIngredient.Name)
                    .SingleAsync();
            }
            else
            {
                await _context.Ingredients.AddAsync(newIngredient);
                _context.SaveChanges();

                return newIngredient;
            }
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            var allIngredients = _context.Ingredients.AsEnumerable();

            return allIngredients;
        }
    }
}
