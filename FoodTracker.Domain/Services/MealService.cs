using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateMealAsync(Meal meal, IEnumerable<Ingredient> ingredients)
        {
            await _context.AddAsync(meal);
            _context.SaveChanges();

            foreach (var ingredient in ingredients)
            {
                int ingredientId;
                if (_context.Ingredients.Any(i => i.Name == ingredient.Name))
                {
                    ingredientId = _context.Ingredients
                        .Where(i => i.Name == ingredient.Name)
                        .Select(i => i.Id)
                        .Single();
                }
                else
                {
                    await _context.Ingredients.AddAsync(ingredient);
                    _context.SaveChanges();

                    ingredientId = ingredient.Id;
                }

                var mealIngredient = new MealIngredient { MealId = meal.Id, IngredientId = ingredientId };

                await _context.MealIngredients.AddAsync(mealIngredient);
            }

            _context.SaveChanges();
        }
    }
}
