using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTracker.Database;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Domain.Services
{
    public class MealService : IMealService
    {
        private readonly FoodTrackerContext _context;

        public MealService(FoodTrackerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            var meals = await _context.Meals.ToListAsync();
            return meals;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsWithIngredientsAsync()
        {
            var mealsWithIngredients = await _context.Meals
                .Include(m => m.MealIngredients).ThenInclude(mi => mi.Ingredient)
                .ToListAsync();

            return mealsWithIngredients;
        }
    }
}
